using BlankSpider.Spider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlankSpider.Api;
using BlankSpider.Api.Entities;
using BlankSpider.Api.Implements;
using Newtonsoft.Json;

namespace VNEXPRESS
{
    public class VNEXPRESSManager : BaseSpiderManagement
    {
        //private Source _source;

        public VNEXPRESSManager()
        {
            SpiderSingletonEvent.Instance.SpiderReloadForUpdate += Instance_SpiderReloadForUpdate;
        }

        private readonly object _lock = new object();

        public VNEXPRESSManager(string name) : base(name) { }


        public VNEXPRESSManager(string name, string sourceid)
            : base(name, sourceid)
        {
            this.Name = name;
            this.SourceId = sourceid;
            LoadConfigGeneral();
            LoadConfigLink();


        }

        private void Instance_SpiderReloadForUpdate(object sender, BlankSpider.Spider.Events.SpiderArgs e)
        {
            lock (this._lock)
            {
                LoadConfigLink();
            }


        }

        public override void LoadConfigGeneral()
        {
            string strConfig = new SourceImpl().GetConfigGeneral(this.SourceId).Result;
            SourceConfigGeneral config = JsonConvert.DeserializeObject<SourceConfigGeneral>(strConfig);
            try
            {
                this._THREAD_NUMBER = 1;
                int.TryParse(config.thread_number, out this._THREAD_NUMBER);

                this._THREAD_SLEEP = 1000;
                int.TryParse(config.thread_sleep, out this._THREAD_SLEEP);

                this._BASE_URL = config.base_url;

                this._MAX_TRYING_COUNT_ = 100;
                int.TryParse(config.max_trying_count, out this._MAX_TRYING_COUNT_);

                this._VIDEO_BASE_URL = config.video_base_url;


                this._POST_URL = config.post_url; 



                this._THREAD_PARSING_NUMBER = 1;
                int.TryParse(config.thread_number_parsing, out this._THREAD_PARSING_NUMBER);

                this._UPDATE_SLEEP = 120000;
                int.TryParse(config.update_sleep, out this._UPDATE_SLEEP);


                this.chk_unique_css = 0;
                int.TryParse(config.chk_unique_css, out this.chk_unique_css);

                this.filter_pdf = config.filter_pdf;
                this.remove_filter_pdf = config.remove_filter_pdf;



            }
            catch (Exception ex)
            {
            }
        }

        public override void LoadConfigLink()
        {
            string strConfig = new SourceImpl().GetConfigLink(this.SourceId).Result;

            try
            {
                if (!string.IsNullOrWhiteSpace(strConfig))
                {
                    configlinks = JsonConvert.DeserializeObject<SourceConfigLink>(strConfig);

                    base.frontierURL.Clear();
                    base.queueDetailURL.Clear();

                    var _configLink = configlinks.configlinks.Where(c => c.link_type == "FIX_LINK").ToList();
                    if (_configLink != null)
                    {

                        foreach (var item in _configLink)
                        {
                            ConcreteLink _concreteLink = new ConcreteLink();
                            _concreteLink.link_type = "FIX_LINK";
                            _concreteLink.href = item.url_pattern;
                            base.frontierURL.Enqueue(_concreteLink);
                            configlinks.configlinks.Remove(item);
                        }
                    }



                    _configLink = configlinks.configlinks.Where(c => c.link_type == "BEGIN_LINK").ToList();
                    if (_configLink != null)
                    {

                        foreach (var item in _configLink)
                        {
                            ConcreteLink _concreteLink = new ConcreteLink();
                            _concreteLink.link_type = "BEGIN_LINK";
                            _concreteLink.href = item.url_pattern;
                            base.frontierURL.Enqueue(_concreteLink);
                            configlinks.configlinks.Remove(item);
                        }
                    }
                }


                var _removeLinks = configlinks.configlinks.Where(c => c.link_type == "REMOVE_LINK").ToList();
                if (_removeLinks != null)
                {
                    foreach (var item in _removeLinks)
                    {
                        _stringbuilder_removeLinks.Append(item.url_pattern + ";");
                        configlinks.configlinks.Remove(item);
                    }
                }



            }
            catch (Exception ex) { }
        }

        public override void LoadConfigField()
        {

            try
            {
                string strConfig = new SourceImpl().GetConfigField(this.SourceId).Result;
                configfields = JsonConvert.DeserializeObject<SourceConfigField>(strConfig);
            }
            catch (Exception ex) { }
        }

        public override void LoadConfigVideo()
        {
            try
            {
                string strConfig = new SourceImpl().GetConfigVideo(this.SourceId).Result;
                configvideos = JsonConvert.DeserializeObject<SourceConfigVideo>(strConfig);
            }
            catch (Exception ex) { }
        }


        public override BaseSpider CreateSpider()
        {
            return new Vnexpress();
        }
    }
}
