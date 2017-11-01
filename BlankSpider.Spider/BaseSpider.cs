using BlankSpider.Api.Entities;
using BlankSpider.Spider.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlankSpider.Spider
{
    public abstract class BaseSpider
    {

        private CounterManager _counterManager;
        public int THREAD_SLEEP = 100;
        public bool IS_STOP = false;
        
        Thread _thread = null;

        public static SortTree _UrlStorage = new SortTree();

        public object _lock = new object();

        public BaseManagement _BaseManagement { get; set; }


        public BaseSpider() { }
        public BaseSpider(string name){
            this.Name = name;
        }

        public BaseSpider(string name, string sourceid)
        {
            this.Name = name;
            this.SourceID = sourceid;
        }

        public object LockObject
        {
            get
            {
                return _lock;
            }
            set
            {
                _lock = value;
            }
        }

        public Queue<ConcreteLink> FrontierURL
        {
            get;set;
        }

        public Queue<ConcreteLink> QueueDetailURL { get; set; }

  

        public SourceConfigLink ConfigLinks { get; set; }
        public StringBuilder RemoveLinks { get; set; }

        public SourceConfigField ConfigFields { get; set; }

        public SourceConfigVideo ConfigVideos { get; set; }

        public string BaseURL { get; set; }
        public string VIDEOBaseURL { get; set; }

        public CounterManager CounterManager
        {
            get
            {
                return _counterManager;
            }
            set
            {
                _counterManager = value;
            }
        }

        public string Name { get; set; }

        public string ThreadName { get; set; }
        public string SourceID { get; set; }
        public string PostURL { get; set; }

        public string VIDEO_POST_URL { get; set; }

        public int chk_unique_css { get; set; }
        public string filter_pdf { get; set; }

        public string remove_filter_pdf { get; set; }

        public string Mode { get; set; }

        public int MaxTryingCount { get; set; }
        public int UpdateSleep { get; set; }


        public abstract void LoadConfigParser();

        public abstract string GetName();
        protected abstract void ProcessFindLink();
        protected abstract void ProcessParseLink();

        public string WorkerType { get; set; }

        public void Start()
        {
            lock (LockObject)
            {
                IS_STOP = false;
                if(WorkerType == "FIND_LINK")
                {
                    _thread = new Thread(new ThreadStart(ProcessFindLink));
                }
                else
                {
                    _thread = new Thread(new ThreadStart(ProcessParseLink));
                }
                
                _thread.Name = ThreadName;
                _thread.Start();
                string message = string.Format("[{0}] STARTED \r\n", ThreadName);
                SpiderSingletonEvent.Instance.OnSpiderStatusChanged(new Events.SpiderArgs() { Message = message });
            }
            
            

        }

        public void Stop()
        {
            lock (LockObject)
            {
                try
                {
                    IS_STOP = true;
                    //_thread.Interrupt();
                    _thread.Abort();
                    //_thread.Join();
                    _thread = null;
                }
                catch (Exception ex)
                {
                    _thread.Abort();
                    //_thread.Join();
                    IS_STOP = true;
                    //_thread.Interrupt();
                    _thread = null;
                }

                string message = string.Format("[{0}] STOPPED \r\n", ThreadName);
                SpiderSingletonEvent.Instance.OnSpiderStatusChanged(new Events.SpiderArgs() { Message = message });
            }
            

        }

        public bool IsNewUrl(ref string url)
        {
            bool bNew = false;
            lock (_UrlStorage)
            {
                try
                {
                    bNew = _UrlStorage.Add(ref url).Count == 1;

                    
                }
                catch (Exception ex)
                {
                    
                }
            }
            return bNew;
        }


        public void UrlStoragClear()
        {
            
            lock (_UrlStorage)
            {
                _UrlStorage.Clear();
            }
           
        }


    }
}
