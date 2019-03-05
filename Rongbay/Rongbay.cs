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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rongbay
{
    public class Rongbay: BaseSpider
    {
        public string _removelinks = string.Empty;
        public string _removelinks_detail = string.Empty;
        public Rongbay() { }

        public Rongbay(string name) : base(name) { }

        public Rongbay(string name, string sourceid) : base(name, sourceid)
        {

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
                    _removelinks_detail = this.RemoveLinks.ToString();

                    string url = string.Empty;
                    if (QueueDetailURL.Count > 0)
                    {
                        ConcreteLink objUrl;

                        lock (QueueDetailURL)
                        {
                            try
                            {
                                objUrl = QueueDetailURL.Dequeue();
                            }
                            catch
                            {
                                continue;
                            }
                        }



                        string html = string.Empty;
                        try
                        {
                            if (string.IsNullOrEmpty(objUrl.href))
                                continue;
                        }
                        catch { continue; }


                        url = objUrl.href;

                        if (!objUrl.href.Contains("https://") && !objUrl.href.Contains("http://"))
                        {
                            url = this.BaseURL + objUrl.href;
                        }


                        try
                        {
                            html = DownloadExpress.getResponseString(url, "");
                        }
                        catch (Exception _error)
                        {
                            //-------------------------------------------------------------------------------------------------------
                            string _errorString = string.Format("{0}-ERROR:{1}-URLDownload:{2}", this.ThreadName, _error.ToString(), url);
                            SpiderArgs args = new SpiderArgs()
                            {
                                Message = _errorString
                            };
                            SpiderSingletonEvent.Instance.OnSpiderScreenConsole(args);

                        }

                        if (string.IsNullOrEmpty(html))
                            continue;


                        #region FIND DETAIL LINK
                        /*foreach (var item in base.ConfigLinks.configlinks)
                        {
                            FindLinkBase findLink = null;
                            if (item.pattern_type == "REGEX")
                            {
                                //if(item.link_type == "REMOVE_LINK")

                                if (_removelinks_detail != string.Empty)
                                {
                                    findLink = new FindLinkByRegex(this.BaseURL, item.url_pattern, _removelinks_detail);
                                }
                                else
                                {
                                    findLink = new FindLinkByRegex(this.BaseURL, item.url_pattern, "");
                                }
                            }
                            

                            if (findLink == null)
                                continue;

                            if (item.link_type == "DETAIL_LINK")
                            {
                                List<string> linkDetail = findLink.FindLink(html);
                                if (linkDetail == null || linkDetail.Count == 0)
                                    continue;


                                foreach (string href in linkDetail)
                                {
                                    string _url = href;
                                    if (IsNewUrl(ref _url))
                                    {
                                        ConcreteLink _concreteLink = new ConcreteLink();
                                        _concreteLink.link_type = "DETAIL_LINK";
                                        _concreteLink.href = href;
                                        QueueDetailURL.Enqueue(_concreteLink);
                                    }
                                }
                            }
                        }*/
                        #endregion


                        #region PARSE FIELD


                        string url_md5 = StringHelpers.MD5Hash(url);

                        //check exist

                        try
                        {
                            string isExist = DownloadExpress.getResponseString(string.Format("http://localhost:9999/ContentHub/CheckExist?url_md5={0}", url_md5), "");
                            bool _isExist = Convert.ToBoolean(isExist);
                            if (_isExist)
                            {
                                CounterManager.InCreaseTryingCount();
                                continue;
                            }
                                
                        }
                        catch (Exception _error)
                        {
                         

                        }
                        //################################################################################



                        var dict_post = new Dictionary<string, object>();
                        dict_post.Add("UrlMd5", url_md5);
                        dict_post.Add("Url", url);

                        Dictionary<string, string> _dataString = new Dictionary<string, string>();
                        string the_last_config = string.Empty;

                        string content_filter = string.Empty;
                        string content_khongdau = string.Empty;

                        #region process parsing
                        try
                        {

                            string title = StringHelpers.GetStringBetween(html, "<h1><p class=\"title font_28\">", "</p></h1>");
                            if (string.IsNullOrEmpty(title))
                                continue;
                            dict_post.Add("Title", title);

                            string Price = StringHelpers.GetStringBetween(html, "<li class=\"li_100 clear icon_bds font_14 roboto_regular\">Giá : <span class=\"font_14 roboto_bold\">", "</span></li>");                                                            
                            dict_post.Add("Price", Price);

                            string Acreage = StringHelpers.GetStringBetween(html, "<li class=\"li_100 clear icon_bds font_14 roboto_regular\">Diện tích : <span class=\"font_14 roboto_bold\">", "<sup>2</sup></span></li>");
                            dict_post.Add("Acreage", Acreage);

                            string Address = StringHelpers.GetStringBetween(html, "<span class=\"roboto_bold font_14 cl_333 dc_new icon_bds\">Địa chỉ: </span>", "<span");
                            dict_post.Add("Address", Address);

                            string Room = StringHelpers.GetStringBetween(html, "<li class=\"li_100 clear icon_bds font_14 roboto_regular\">Có <span class=\"font_14 roboto_bold\">", "</span></li>");
                            dict_post.Add("Room", Room);

                            string Phone = StringHelpers.GetStringBetween(html, "<p id=\"mobile_show\" class=\"mobile_hide show_mobile\" style=\"*display: none; \"><span style=\"*padding-left: 10px\">", "</span></p>");
                            dict_post.Add("Phone", Phone);


                            string Content = StringHelpers.GetStringBetween(html, "<div class=\"info_text\">", "<div class=\"info_box\">");
                            Content = HtmlUtility.RemoveAllTag(Content);
                            dict_post.Add("Content", Content);

                            dict_post.Add("Location", "Ha Noi");

                            dict_post.Add("Source", "Rongbay");


                            string DatePost = StringHelpers.GetStringBetween(html, "Thời gian đăng : <span class=\"color cl_888 font_13\">", " -");
                            dict_post.Add("DatePost", DatePost);

                            string CategoryName = StringHelpers.GetStringBetween(html, "<span class=\"nameScate\">", "</span>");
                            CategoryName = HtmlUtility.RemoveAllTag(CategoryName);
                            dict_post.Add("CategoryName", CategoryName);
                            
                            dict_post.Add("Metadata", html);







                            string jsonContent = JsonConvert.SerializeObject(dict_post, Formatting.Indented);

                            if (!string.IsNullOrWhiteSpace(jsonContent))
                            {
                                string results = string.Empty;
                                if (this.Mode != "TEST") // if not test then insert to api db
                                {
                                    
                                    string _url_post = String.Join("&", dict_post.Select(i => $"{i.Key}={i.Value}"));

                                    results = DownloadExpress.DownloadPost(this.PostURL, jsonContent);
                                    if (!string.IsNullOrEmpty(results)) //check data return from api if not null or empty
                                    {
                                        try
                                        {
                                            var _info = JsonConvert.DeserializeObject<ContentHub>(results);

                                            if(_info.Id > 0)
                                            {
                                                this._BaseManagement.TotalInsertLink = this._BaseManagement.TotalInsertLink + 1;
                                                CounterManager.ResetTryingCount();
                                            }
                                            else
                                            {

                                            }

                                           
                                        }
                                        catch (Exception error)
                                        {
                                            string message = string.Format("[{0}] ERROR: {1} , {2}, {3}", ThreadName, error.Message, DateTime.Now, url);
                                            SpiderSingletonEvent.Instance.OnSpiderScreenConsole(new BlankSpider.Spider.Events.SpiderArgs() { Message = message });
                                            CounterManager.InCreaseTryingCount();
                                            continue;
                                        }
                                    }
                                    else //empty or error
                                    {
                                        CounterManager.InCreaseTryingCount();
                                    }

                                }

                                try
                                {
                                    SpiderArgs args = new SpiderArgs()
                                    {
                                        SourceName = this.ThreadName,
                                        Index = number,
                                        Href = url,
                                        Title = (string)dict_post["Title"],
                                        Message = results
                                    };
                                    SpiderSingletonEvent.Instance.OnSpiderProcessing(args);
                                }
                                catch { }

                            }
                        }
                        catch (Exception _errorParsing)
                        {
                            string message = string.Format("[{0}][PROCESS-PARSING] ERROR: {1} , {2}, {3}", ThreadName, _errorParsing.Message, DateTime.Now, url);
                            SpiderSingletonEvent.Instance.OnSpiderScreenConsole(new BlankSpider.Spider.Events.SpiderArgs() { Message = message });
                            CounterManager.InCreaseTryingCount();
                            continue;
                        }
                        #endregion



                        #endregion
                    }
                    else
                    {
                        CounterManager.InCreaseTryingCount();
                    }



                    if (this.Mode == "UPDATE")
                    {
                        if (number >= 2000)
                        {
                            CounterManager.CurrentTryingCount = 100000;
                        }

                        if (CounterManager.CurrentTryingCount >= this.MaxTryingCount)
                        {

                            string message = string.Format("[{0}] SLEEP FOR UPDATE AFTER {1} seconds, {2}", ThreadName, (this.UpdateSleep / 1000), DateTime.Now);
                            SpiderSingletonEvent.Instance.OnSpiderScreenConsole(new BlankSpider.Spider.Events.SpiderArgs() { Message = message });


                            Thread.Sleep(this.UpdateSleep);

                            try
                            {
                                //this._BaseManagement.TotalUpdateLink = 0;
                                //this._BaseManagement.TotalInsertLink = 0;
                                //UrlStoragClear();
                                //CounterManager.ResetTryingCount();
                                CounterManager.Reset(0, 100);
                            }
                            catch
                            {
                                //this._BaseManagement.TotalUpdateLink = 0;
                                //this._BaseManagement.TotalInsertLink = 0;
                                //UrlStoragClear();
                                //CounterManager.ResetTryingCount();
                                CounterManager.Reset(0, 100);
                            }





                        }
                    }
                    Thread.Sleep(this.THREAD_SLEEP);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("[{0}] ERROR OUT OF THREAD: {1} , {2}", ThreadName, ex.Message.ToString(), DateTime.Now);
                SpiderSingletonEvent.Instance.OnSpiderScreenConsole(new BlankSpider.Spider.Events.SpiderArgs() { Message = message });
            }
        }

        protected override void ProcessFindLink()
        {
            try
            {
                while (!IS_STOP)
                {
                    //int number = CounterManager.GetNumber();
                    _removelinks = this.RemoveLinks.ToString();
                    string url = string.Empty;
                    if (FrontierURL.Count > 0)
                    {
                        ConcreteLink objUrl;
                        lock (FrontierURL)
                        {
                            try
                            {
                                objUrl = FrontierURL.Dequeue();
                            }
                            catch { continue; }
                        }



                        string html = string.Empty;

                        if (string.IsNullOrEmpty(objUrl.href))
                            continue;

                        url = objUrl.href;

                        if (!objUrl.href.Contains("https://") && !objUrl.href.Contains("http://"))
                        {
                            url = this.BaseURL + objUrl.href;
                        }

                        string baseURL = string.Empty;
                        try
                        {

                            //html = DownloadExpress.Download(url, Utility.GetEncoding(EncodingType.UTF8));
                            html = DownloadExpress.getResponseString(url, "", out baseURL);

                        }
                        catch (Exception _error)
                        {
                            //-------------------------------------------------------------------------------------------------------
                            string _errorString = string.Format("{0}-ERROR:{1}-URLDownload:{2}", this.ThreadName, _error.ToString(), url);
                            SpiderArgs args = new SpiderArgs()
                            {
                                Message = _errorString
                            };
                            SpiderSingletonEvent.Instance.OnSpiderScreenConsole(args);
                            //--------------------------------------------------------------------------------------------------------
                            try
                            {

                                html = DownloadExpress.getResponseString(url, "", out baseURL);
                            }
                            catch (Exception ex)
                            {
                                _errorString = string.Format("{0}-ERROR:{1}-URLDownloadBySocket:{2}", this.ThreadName, ex.ToString(), url);
                                args = new SpiderArgs()
                                {
                                    Message = _errorString
                                };
                                SpiderSingletonEvent.Instance.OnSpiderScreenConsole(args);
                                //--------------------------------------------------------------------------------------------------------
                                continue;
                            }
                        }

                        if (string.IsNullOrEmpty(html))
                            continue;


                        var _configLinkDetailLink = base.ConfigLinks.configlinks.Where(c => c.link_type == "DETAIL_LINK").ToList();

                        foreach (var item in base.ConfigLinks.configlinks)
                        {
                            FindLinkBase findLink = null;
                            if (item.pattern_type == "REGEX")
                            {
                                //if(item.link_type == "REMOVE_LINK")

                                if (_removelinks != string.Empty)
                                {
                                    findLink = new FindLinkByRegex(this.BaseURL, item.url_pattern, _removelinks);
                                }
                                else
                                {
                                    findLink = new FindLinkByRegex(this.BaseURL, item.url_pattern, "");
                                }
                            }
                            else if (item.pattern_type == "XPATH")
                            {
                                if (_removelinks != string.Empty)
                                {
                                    findLink = new FindLinkByXPath(this.BaseURL, item.url_pattern, _removelinks);
                                }
                                else
                                {
                                    findLink = new FindLinkByXPath(this.BaseURL, item.url_pattern, "");
                                }
                            }

                            if (findLink == null)
                                continue;


                            #region SUB_LINK
                            if (item.link_type == "SUB_LINK")
                            {
                                List<string> listLinkSub = findLink.FindLink(html);
                                if (listLinkSub == null || listLinkSub.Count == 0)
                                    continue;

                                List<string> listSubLinkRemove = new List<string>();
                                //##################### filter sub link ##############################
                                //#####################################################################

                                if (listSubLinkRemove.Count > 0)
                                {
                                    listLinkSub = listLinkSub.Where(s => !listSubLinkRemove.Contains(s)).ToList();
                                }

                                foreach (string linkSub in listLinkSub)
                                {
                                    string _link = linkSub;
                                    if (!_link.Contains("http://") && !_link.Contains("https://"))
                                    {
                                        _link = baseURL + linkSub;
                                    }


                                    string _url = _link;
                                    if (IsNewUrl(ref _url))
                                    {

                                        ConcreteLink _concreteLink = new ConcreteLink();
                                        _concreteLink.link_type = "SUB_LINK";
                                        _concreteLink.href = _url;
                                        FrontierURL.Enqueue(_concreteLink);
                                        SpiderArgs args = new SpiderArgs()
                                        {
                                            Message = string.Format("{0}-SUB-{1}", _url, this.ThreadName)
                                        };
                                        SpiderSingletonEvent.Instance.OnSpiderInformation(args);
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
                                    string _link = href;
                                    if (!_link.Contains("http://") && !_link.Contains("https://"))
                                    {
                                        _link = baseURL + href;
                                    }

                                    string _url = _link;
                                    if (IsNewUrl(ref _url))
                                    {
                                        ConcreteLink _concreteLink = new ConcreteLink();
                                        _concreteLink.link_type = "DETAIL_LINK";
                                        _concreteLink.href = _url;
                                        QueueDetailURL.Enqueue(_concreteLink);
                                    }
                                }
                            }
                            #endregion
                        }


                    }
                    if (this.Mode == "UPDATE")
                    {
                        if (CounterManager.CurrentTryingCount >= this.MaxTryingCount)
                        {

                            string message = string.Format("[{0}] SLEEP FOR UPDATE AFTER {1} seconds, {2}", ThreadName, (this.UpdateSleep / 1000), DateTime.Now);
                            SpiderSingletonEvent.Instance.OnSpiderScreenConsole(new BlankSpider.Spider.Events.SpiderArgs() { Message = message });
                            SpiderSingletonEvent.Instance.OnSpiderReloadForUpdate(new BlankSpider.Spider.Events.SpiderArgs()
                            { Message = string.Format("{0} RELOAD FOR UPDATE \r\n", ThreadName) });

                            Thread.Sleep(this.UpdateSleep);

                            try
                            {
                                this._BaseManagement.TotalUpdateLink = 0;
                                this._BaseManagement.TotalInsertLink = 0;
                                //CounterManager.ResetTryingCount();
                                //CounterManager.CurrentNumber = 0;
                                UrlStoragClear();
                                CounterManager.Reset(0, 100);
                            }
                            catch
                            {
                                this._BaseManagement.TotalUpdateLink = 0;
                                this._BaseManagement.TotalInsertLink = 0;
                                //CounterManager.ResetTryingCount();
                                //CounterManager.CurrentNumber = 0;
                                UrlStoragClear();
                                CounterManager.Reset(0, 100);
                            }



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
