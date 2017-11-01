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

namespace DefaultTemplate
{
    public class DefaultManager: BaseSpiderManagement
    {
        //private Source _source;

        public DefaultManager() { }

        public DefaultManager(string name):base(name) { }

        public DefaultManager(string name, string sourceid)
            : base(name, sourceid)
        {
            this.Name = name;
            this.SourceId = sourceid;
            LoadConfigGeneral();
            LoadConfigLink();
        }

        public override void LoadConfigGeneral()
        {
            string strConfig = new SourceImpl().GetConfigGeneral(this.SourceId).Result;
            SourceConfigGeneral config = JsonConvert.DeserializeObject<SourceConfigGeneral>(strConfig);
            try {
                this._THREAD_NUMBER = 1;
                int.TryParse(config.thread_number, out this._THREAD_NUMBER);

                this._THREAD_SLEEP = 1000;
                int.TryParse(config.thread_sleep, out this._THREAD_SLEEP);

                this._BASE_URL = config.base_url;

                this._MAX_TRYING_COUNT_ = 100;

                this._POST_URL = config.post_url;

                int.TryParse(config.max_trying_count, out this._MAX_TRYING_COUNT_);

                this.chk_unique_css = 0;
                int.TryParse(config.chk_unique_css, out this.chk_unique_css);

                this.filter_pdf = config.filter_pdf;
                this.remove_filter_pdf = config.remove_filter_pdf;


            }
            catch (Exception ex) { 
            }
        }

        public override void LoadConfigLink()
        {
            string strConfig = new SourceImpl().GetConfigLink(this.SourceId).Result;
            configlinks = JsonConvert.DeserializeObject<SourceConfigLink>(strConfig);
            try {

                var _configLink = configlinks.configlinks.Where(c => c.link_type == "BEGIN_LINK").ToList();
                if (_configLink != null)
                {
                    foreach(var item in _configLink)
                    {
                        ConcreteLink _concreteLink = new ConcreteLink();
                        _concreteLink.link_type = "BEGIN_LINK";
                        _concreteLink.href = item.url_pattern;
                        base.frontierURL.Enqueue(_concreteLink);
                        configlinks.configlinks.Remove(item);
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
            catch(Exception ex) { }
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
            return new Default();
        }
    }
}
