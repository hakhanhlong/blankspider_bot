using BlankSpider.Api.Entities;
using BlankSpider.Spider.Enums;
using BlankSpider.Spider.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankSpider.Spider
{
    public abstract class BaseSpiderManagement
    {

        #region General configurations
        public int _THREAD_NUMBER = 1;
        public int _NUMBER_THREADS_CONCURRENT = 1;
        public int _TRYING_COUNT_ = 0;
        public int _MAX_TRYING_COUNT_ = 100;
        public int _START_ID_ = 0;
        public int _COUNTER_ = 0;
        public int _CURRENT_NUMBER_ = 0;
        public int _THREAD_SLEEP = 1000;
        public int _UPDATE_SLEEP = 1000;
        public int _THREAD_PARSING_NUMBER = 1;
        public string _BASE_URL = string.Empty;

        public string _VIDEO_BASE_URL = string.Empty;

        public string _POST_URL = string.Empty;
        public string _VIDEO_POST_URL = string.Empty;

        public string filter_pdf = string.Empty;
        public string remove_filter_pdf = string.Empty;
        public int chk_unique_css = 0;

        #endregion




        public static object _lock = new object();
        public CounterManager _CounterManager = new CounterManager(0);
        public BaseManagement _BaseManagement = new BaseManagement();
        public MODE_SPIDER _MODE_SPIDER = MODE_SPIDER.GETNEW;
        public SOURCE_STATUS _SOURCE_STATUS = SOURCE_STATUS.NOTHING;
        public THREAD_STATUS _THREAD_STATUS = THREAD_STATUS.NOTHING;
        Hashtable _listBaseSpider = new Hashtable();


        public Queue<ConcreteLink> frontierURL = new Queue<ConcreteLink>();
        public Queue<ConcreteLink> queueDetailURL = new Queue<ConcreteLink>();
        public SourceConfigLink configlinks = new SourceConfigLink();
        public SourceConfigField configfields = new SourceConfigField();
        public SourceConfigVideo configvideos = new SourceConfigVideo();
        public StringBuilder _stringbuilder_removeLinks = new StringBuilder();


       


        public enum MODE_SPIDER { 
            GETNEW,
            UPDATE,
            TEST
        }

        public enum THREAD_STATUS : byte { 
            RUNNING,
            PAUSED,
            STARTING,
            NOTHING
        }

        

        public MODE_SPIDER Mode { get; set; }


        public BaseSpiderManagement() {
            
        }

        public BaseSpiderManagement(string name) {
            this.Name = name;
        }

        public BaseSpiderManagement(string name, string id) {
            this.Name = name;
            this.SourceId = id;
        }

        public string Name { get; set; }
        public string SourceId { get; set; }


        public SOURCE_STATUS SourceStatus { get { return _SOURCE_STATUS; } }
        public void SetSourceStatus(SOURCE_STATUS status)
        {
            lock (_lock)
            {
                _SOURCE_STATUS = status;


                if (status == SOURCE_STATUS.RUNNING || status == SOURCE_STATUS.PAUSED)
                {
                    string message = string.Format("[{0}] changed status to {1} \r\n", this.Name, _SOURCE_STATUS.ToString());
                    SpiderSingletonEvent.Instance.OnSourceStatusChanged(new SpiderManagementArgs() { Message = message, SourceStatus = status });
                }
                else
                {
                    string message = string.Format("[{0}] changed status to {1} \r\n", this.Name, _SOURCE_STATUS.ToString());
                    SpiderSingletonEvent.Instance.OnSourceStatusChanged(new SpiderManagementArgs() { Message = message });
                }
            }
            

            
        }


        public abstract void LoadConfigGeneral();
        public abstract void LoadConfigLink();

        public abstract void LoadConfigField();

        public abstract void LoadConfigVideo();


        public abstract BaseSpider CreateSpider();


        public void Start()
        {
            SetSourceStatus(SOURCE_STATUS.STARTING);

            for (int i = 0; i < this._THREAD_NUMBER; i++)
            {
                var _BaseSpider = CreateSpider();

                _BaseSpider.Name = this.Name;
                _BaseSpider.SourceID = this.SourceId;
                _BaseSpider.ThreadName = this.Name + "- Thread(FINDLINK) " + i;
                _BaseSpider.THREAD_SLEEP = _THREAD_SLEEP;
                _BaseSpider.CounterManager = _CounterManager;
                _BaseSpider._BaseManagement = _BaseManagement;

                _BaseSpider.Mode = this.Mode.ToString();
                _BaseSpider.WorkerType = "FIND_LINK";

                _BaseSpider.FrontierURL = this.frontierURL;
                _BaseSpider.QueueDetailURL = this.queueDetailURL;
                _BaseSpider.PostURL = _POST_URL;
                _BaseSpider.LockObject = _lock;
                _BaseSpider.MaxTryingCount = this._MAX_TRYING_COUNT_;
                _BaseSpider.UpdateSleep = this._UPDATE_SLEEP;

                _BaseSpider.VIDEOBaseURL = this._VIDEO_BASE_URL;
                

                //configs ######################################################
                _BaseSpider.ConfigLinks = configlinks;
                _BaseSpider.ConfigFields = configfields;
                _BaseSpider.ConfigVideos = configvideos;
                _BaseSpider.RemoveLinks = _stringbuilder_removeLinks;

                _BaseSpider.BaseURL = this._BASE_URL;

                _BaseSpider.chk_unique_css = this.chk_unique_css;
                _BaseSpider.filter_pdf = this.filter_pdf;
                _BaseSpider.remove_filter_pdf = this.remove_filter_pdf;

                _BaseSpider.VIDEO_POST_URL = this._VIDEO_POST_URL;

                //##############################################################
                _BaseSpider.Start();
                _listBaseSpider[string.Format("{0}_thread_{1}_findlink", this.SourceId, i)] = _BaseSpider;
            }

            for (int i = 0; i < this._THREAD_PARSING_NUMBER; i++)
            {
                var _BaseSpider = CreateSpider();

                _BaseSpider.Name = this.Name;
                _BaseSpider.SourceID = this.SourceId;
                _BaseSpider.ThreadName = this.Name + "- Thread(PARSING) " + i;
                _BaseSpider.THREAD_SLEEP = _THREAD_SLEEP;
                _BaseSpider.CounterManager = _CounterManager;
                _BaseSpider._BaseManagement = _BaseManagement;

                _BaseSpider.Mode = this.Mode.ToString();
                _BaseSpider.WorkerType = "PARSE_LINK";

                _BaseSpider.FrontierURL = this.frontierURL;
                _BaseSpider.QueueDetailURL = this.queueDetailURL;
                _BaseSpider.PostURL = _POST_URL;
                _BaseSpider.LockObject = _lock;

                _BaseSpider.MaxTryingCount = this._MAX_TRYING_COUNT_;
                _BaseSpider.UpdateSleep = this._UPDATE_SLEEP;


                //configs ######################################################
                _BaseSpider.ConfigLinks = configlinks;
                _BaseSpider.ConfigFields = configfields;
                _BaseSpider.ConfigVideos = configvideos;
                _BaseSpider.RemoveLinks = _stringbuilder_removeLinks;

                _BaseSpider.chk_unique_css = this.chk_unique_css;
                _BaseSpider.filter_pdf = this.filter_pdf;
                _BaseSpider.remove_filter_pdf = this.remove_filter_pdf;


                _BaseSpider.BaseURL = this._BASE_URL;
                _BaseSpider.VIDEOBaseURL = this._VIDEO_BASE_URL;

                _BaseSpider.VIDEO_POST_URL = this._VIDEO_POST_URL;

                //##############################################################
                _BaseSpider.Start();
                _listBaseSpider[string.Format("{0}_thread_{1}_parsing", this.SourceId, i)] = _BaseSpider;
            }


            SetSourceStatus(SOURCE_STATUS.RUNNING);
            
        }

        public void Stop()
        {

            SetSourceStatus(SOURCE_STATUS.PAUSING);
            foreach (BaseSpider item in _listBaseSpider.Values)
            {
                item.Stop();
            }


            SetSourceStatus(SOURCE_STATUS.PAUSED);

            
        }
        

    }
}
