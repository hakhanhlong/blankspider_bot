using BlankSpider.Api.Entities;
using BlankSpider.Spider;
using BlankSpider.Spider.Events;
using BlankSpider.Spider.FindLinks;
using BlankSpider.Spider.HtmlRequest;
using BlankSpider.Spider.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DefaultTemplate
{
    public class Default: BaseSpider
    {
        public string _removelinks = string.Empty;
        public Default() { }

        public Default(string name): base(name) { }

        public Default(string name, string sourceid) : base(name, sourceid) {
            
        }

        public override void LoadConfigParser()
        {

        }

        

        public override string GetName()
        {
            return this.Name;
        }

        protected override void ProcessParseLink()
        {
            try
            {
                while (!IS_STOP)
                {
                    int number = CounterManager.GetNumber();
                    
                    string url = string.Empty;
                    if (QueueDetailURL.Count > 0)
                    {
                        ConcreteLink objUrl = QueueDetailURL.Dequeue();
                        string html = string.Empty;
                        try {
                            if (string.IsNullOrEmpty(objUrl.href))
                                continue;
                        }
                        catch { continue; }
                        

                        url = objUrl.href;

                        if (!objUrl.href.Contains("https://") && !objUrl.href.Contains("http://"))
                            url = this.BaseURL + objUrl.href;

                        try
                        {
                            html = DownloadExpress.Download(url, Utility.GetEncoding(EncodingType.UTF8));
                        }
                        catch
                        {
                            try
                            {
                                html = DownloadExpress.DownloadBySocket(url, Utility.GetEncoding(EncodingType.UTF8));
                            }
                            catch{continue;}
                        }

                        if (string.IsNullOrEmpty(html))
                            continue;





                        #region PARSE FIELD


                        var dict_post = new Dictionary<string, object>();
                        dict_post.Add("source_id", this.SourceID);
                        dict_post.Add("href", url);

                        Dictionary<string, string> _dataString = new Dictionary<string, string>();

                        foreach (var itemfield in base.ConfigFields.configfields)
                        {
                            string field_name = itemfield.name;
                            string htmlParser = html;
                            string content_text = "";
                            foreach (var config in itemfield.config.step)
                            {
                                htmlParser = StringHelpers.GetStringBetween(htmlParser, config.Value.start_pattern, config.Value.end_pattern);
                                content_text = htmlParser;
                            }


                            if (!string.IsNullOrWhiteSpace(content_text))
                            {
                                if (field_name.ToLower() == "title")
                                {
                                    SpiderArgs args = new SpiderArgs()
                                    {
                                        SourceName = this.ThreadName,
                                        Index = number,
                                        Href = url,
                                        Title = content_text,
                                        Message = string.Empty
                                    };
                                    SpiderSingletonEvent.Instance.OnSpiderProcessing(args);
                                }

                                if (field_name.ToLower() == "tag_name")
                                {
                                    dict_post.Add("tag_name", content_text);
                                }

                                _dataString.Add(field_name, content_text);

                            }

                        }


                        _dataString.Add("html_data", html);
                        string jsonContent = JsonConvert.SerializeObject(_dataString, Formatting.Indented);
                        dict_post.Add("data", jsonContent);
                        jsonContent = JsonConvert.SerializeObject(dict_post, Formatting.Indented);
                        if (!string.IsNullOrWhiteSpace(jsonContent))
                        {
                            string results = DownloadExpress.DownloadPost(this.PostURL, jsonContent);
                        }



                        #endregion





                    }

                    Thread.Sleep(this.THREAD_SLEEP);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        protected override void ProcessFindLink()
        {
            try
            {
                while (!IS_STOP)
                {
                    int number = CounterManager.GetNumber();
                    //_removelinks = this.RemoveLinks.ToString();
                    string url = string.Empty;
                    if(FrontierURL.Count > 0)
                    {
                        ConcreteLink objUrl = FrontierURL.Dequeue();
                        string html = string.Empty;

                        if (string.IsNullOrEmpty(objUrl.href))
                            continue;

                        url = objUrl.href;

                        if (!objUrl.href.Contains("https://") && !objUrl.href.Contains("http://"))
                            url = this.BaseURL + objUrl.href;

                        try {

                            html = DownloadExpress.Download(url, Utility.GetEncoding(EncodingType.UTF8));

                        }
                        catch {
                            try
                            {
                                html = DownloadExpress.DownloadBySocket(url, Utility.GetEncoding(EncodingType.UTF8));
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }

                        if (string.IsNullOrEmpty(html))
                            continue;

                        

                        foreach(var item in base.ConfigLinks.configlinks)
                        {
                            FindLinkBase findLink = null;
                            if (item.pattern_type == "REGEX")
                            {
                                //if(item.link_type == "REMOVE_LINK")
                                
                                if(_removelinks!= string.Empty)
                                {
                                    findLink = new FindLinkByRegex(this.BaseURL, item.url_pattern, _removelinks);
                                }
                                else
                                {
                                    findLink = new FindLinkByRegex(this.BaseURL, item.url_pattern, "");
                                }
                            }

                            if (findLink == null)
                                continue;


                            #region SUB_LINK
                            if(item.link_type == "SUB_LINK")
                            {
                                List<string> listLinkSub = findLink.FindLink(html);
                                if (listLinkSub == null || listLinkSub.Count == 0)
                                    continue;


                                foreach(string linkSub in listLinkSub)
                                {
                                    string _url = linkSub;
                                    if (IsNewUrl(ref _url)) //check exist link
                                    {
                                        if (!_url.Contains(".html#") && !_url.Contains(".htm#"))
                                        {

                                            ConcreteLink _concreteLink = new ConcreteLink();
                                            _concreteLink.link_type = "SUB_LINK";
                                            _concreteLink.href = linkSub;
                                            FrontierURL.Enqueue(_concreteLink);

                                            SpiderArgs args = new SpiderArgs()
                                            {
                                                Message = linkSub + "\r\n"
                                            };


                                            SpiderSingletonEvent.Instance.OnSpiderInformation(args);
                                        }
                                    }
                                  
                                    
                                   
                                }
                            }
                            #endregion

                            #region DETAIL_LINK
                            else if (item.link_type == "DETAIL_LINK")
                            {
                                List<string> linkDetail = findLink.FindLink(html);
                                if (linkDetail == null || linkDetail.Count == 0)
                                    continue;


                                foreach (string href in linkDetail)
                                {
                                    string _url = href;
                                    if (IsNewUrl(ref _url)) //check exist link
                                    {
                                        if (!_url.Contains(".html#") && !_url.Contains(".htm#"))
                                        {


                                            ConcreteLink _concreteLink = new ConcreteLink();
                                            _concreteLink.link_type = "DETAIL_LINK";
                                            _concreteLink.href = href;

                                            QueueDetailURL.Enqueue(_concreteLink);


                                            SpiderArgs args = new SpiderArgs()
                                            {
                                                Message = _url + " - DETAIL" + "\r\n"
                                            };


                                            SpiderSingletonEvent.Instance.OnSpiderInformation(args);
                                        }
                                    }
                                  
                                }
                            }
                            #endregion
                        }

                        





                    }

                    Thread.Sleep(this.THREAD_SLEEP);
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
            
        }


    }
}
