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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ArchiviedTemplate
{
    public class Archivied: BaseSpider
    {
        public string _removelinks = string.Empty;
        public string _removelinks_detail = string.Empty;
        public Archivied() { }

        public Archivied(string name): base(name) { }

        public Archivied(string name, string sourceid) : base(name, sourceid) {

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
                            try {
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
                            url = this.BaseURL + objUrl.href;

                        try
                        {
                            html = DownloadExpress.getResponseString(url, "");
                        }
                        catch(Exception _error)
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


                        //#region FIND DETAIL LINK
                        //foreach (var item in base.ConfigLinks.configlinks)
                        //{
                        //    FindLinkBase findLink = null;
                        //    if (item.pattern_type == "REGEX")
                        //    {
                        //        //if(item.link_type == "REMOVE_LINK")

                        //        if (_removelinks_detail != string.Empty)
                        //        {
                        //            findLink = new FindLinkByRegex(this.BaseURL, item.url_pattern, _removelinks_detail);
                        //        }
                        //        else
                        //        {
                        //            findLink = new FindLinkByRegex(this.BaseURL, item.url_pattern, "");
                        //        }
                        //    }

                        //    if(item.pattern_type == "XPATH")
                        //    {
                        //        if (_removelinks_detail != string.Empty)
                        //        {
                        //            findLink = new FindLinkByXPath(this.BaseURL, item.url_pattern, _removelinks_detail);
                        //        }
                        //        else
                        //        {
                        //            findLink = new FindLinkByXPath(this.BaseURL, item.url_pattern, "");
                        //        }
                        //    }

                        //    if (findLink == null)
                        //        continue;

                        //    if (item.link_type == "DETAIL_LINK")
                        //    {
                        //        List<string> linkDetail = findLink.FindLink(html);
                        //        if (linkDetail == null || linkDetail.Count == 0)
                        //            continue;


                        //        foreach (string href in linkDetail)
                        //        {
                        //            string _url = href;
                        //            if (IsNewUrl(ref _url))
                        //            {
                        //                ConcreteLink _concreteLink = new ConcreteLink();
                        //                _concreteLink.link_type = "DETAIL_LINK";
                        //                _concreteLink.href = href;
                        //                QueueDetailURL.Enqueue(_concreteLink);
                        //            }
                        //        }
                        //    }
                        //}
                        //#endregion


                        #region PARSE FIELD


                        var dict_post = new Dictionary<string, object>();
                        dict_post.Add("source_id", this.SourceID);
                        dict_post.Add("href", url);

                        Dictionary<string, string> _dataString = new Dictionary<string, string>();

                        string content_filter = string.Empty;
                        string content_khongdau = string.Empty;

                        #region process parsing
                        try
                        {
                            //------------------------------- video ------------------------------------------------------------
                            try
                            {
                                //process parsing video
                                foreach (var itemvideo in base.ConfigVideos.configvideos)
                                {
                                    string field_name = itemvideo.name;
                                    string pattern_type = itemvideo.config.pattern_type;
                                    if (pattern_type == PatternType.XPATH.ToString()) //pattern type xpath
                                    {
                                        foreach (var config in itemvideo.config.step)
                                        {
                                            string video_url = XPATHHelpers.ParsingHTML(html, config.Value.field_value);
                                            int is_url_video_cache = 0;
                                            int.TryParse(config.Value.is_url_video_cache, out is_url_video_cache);
                                            if (!string.IsNullOrEmpty(video_url))
                                            {
                                                if (is_url_video_cache == 1) //if video url is cache
                                                {

                                                    var dict_video_post = new Dictionary<string, object>();
                                                    dict_video_post.Add("content_id", "");
                                                    dict_video_post.Add("source_id", this.SourceID);
                                                    dict_video_post.Add("video_url", video_url);
                                                    dict_video_post.Add("video_url_cache", video_url);
                                                    dict_video_post.Add("status", "NEED_CACHE");
                                                    dict_video_post.Add("video_url_path", "empty");                                                    
                                                    dict_video_post.Add("status_download", "PREPARING");

                                                    string json_video_Content = JsonConvert.SerializeObject(dict_video_post, Formatting.Indented);

                                                    var response_video = DownloadExpress.DownloadPost(this.VIDEO_POST_URL, json_video_Content);
                                                }
                                                else
                                                {
                                                    var dict_video_post = new Dictionary<string, object>();
                                                    dict_video_post.Add("content_id", "");
                                                    dict_video_post.Add("source_id", this.SourceID);
                                                    dict_video_post.Add("video_url", video_url);
                                                    dict_video_post.Add("video_url_cache", "empty");
                                                    dict_video_post.Add("status", "NEED_PARSING");
                                                    dict_video_post.Add("video_url_path", "empty");
                                                    dict_video_post.Add("status_download", "PREPARING");

                                                    string json_video_Content = JsonConvert.SerializeObject(dict_video_post, Formatting.Indented);
                                                    var response_video = DownloadExpress.DownloadPost(this.VIDEO_POST_URL, json_video_Content);
                                                }
                                            }


                                        }

                                    }

                                }
                            }
                            catch { }
                            //---------------------------------------------------------------------------------------------------


                            foreach (var itemfield in base.ConfigFields.configfields)
                            {
                                string field_name = itemfield.name;
                                string htmlParser = html;
                                string content_text = "";
                                string the_last_config = string.Empty;


                                string pattern_type = itemfield.config.pattern_type;
                                if (pattern_type == PatternType.XPATH.ToString()) //pattern type xpath
                                {

                                    foreach (var config in itemfield.config.step)
                                    {
                                        //################# check break_parsing point ##############################
                                        bool break_parsing = false;
                                        try
                                        {
                                            if (!string.IsNullOrWhiteSpace(config.Value.break_parsing))
                                            {
                                                if (Convert.ToInt32(config.Value.break_parsing) == 0)
                                                {
                                                    break_parsing = false;
                                                }
                                                else
                                                {
                                                    break_parsing = true;
                                                }

                                            }
                                        }
                                        catch { break_parsing = false; }
                                        //###########################################################################

                                        if (break_parsing) //breaking parsing
                                        {
                                            htmlParser = XPATHHelpers.ParsingHTML(html, config.Value.xpath);
                                            if (!string.IsNullOrWhiteSpace(htmlParser))
                                            {
                                                content_text = htmlParser;
                                                if (field_name == "content")
                                                {
                                                    the_last_config = config.Value.xpath;
                                                }

                                            }

                                        }
                                        else
                                        {
                                            htmlParser = XPATHHelpers.ParsingHTML(htmlParser, config.Value.xpath);
                                            if (!string.IsNullOrWhiteSpace(htmlParser))
                                            {
                                                content_text = htmlParser;
                                                if (field_name == "content")
                                                {
                                                    the_last_config = config.Value.xpath;
                                                }
                                            }
                                        }


                                        //################## process remove html tag ###############################
                                        try
                                        {
                                            if (!string.IsNullOrWhiteSpace(config.Value.remove_html))
                                            {
                                                if (Convert.ToInt32(config.Value.remove_html) == 1)
                                                {
                                                    if (!string.IsNullOrWhiteSpace(htmlParser))
                                                    {
                                                        content_text = HtmlUtility.RemoveAllTag(htmlParser);

                                                    }

                                                }
                                            }
                                        }
                                        catch { }
                                        //##########################################################################
                                    }


                                }
                                else // STRING BETWEEN
                                {
                                    foreach (var config in itemfield.config.step)
                                    {
                                        //################# check break_parsing point ##############################
                                        bool break_parsing = false;
                                        try
                                        {
                                            if (!string.IsNullOrWhiteSpace(config.Value.break_parsing))
                                            {
                                                if (Convert.ToInt32(config.Value.break_parsing) == 0)
                                                {
                                                    break_parsing = false;
                                                }
                                                else
                                                {
                                                    break_parsing = true;
                                                }

                                            }
                                        }
                                        catch { break_parsing = false; }
                                        //###########################################################################

                                        if (break_parsing) //breaking parsing
                                        {
                                            if (string.IsNullOrWhiteSpace(content_text))
                                            {
                                                htmlParser = StringHelpers.GetStringBetween(html, config.Value.start_pattern, config.Value.end_pattern);
                                                if (!string.IsNullOrWhiteSpace(htmlParser))
                                                {
                                                    content_text = htmlParser;
                                                    the_last_config = config.Value.start_pattern;
                                                }
                                            }
                                            else
                                            {
                                                htmlParser = StringHelpers.GetStringBetween(content_text, config.Value.start_pattern, config.Value.end_pattern);
                                                if (!string.IsNullOrWhiteSpace(htmlParser))
                                                {
                                                    content_text = htmlParser;
                                                    the_last_config = config.Value.start_pattern;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            htmlParser = StringHelpers.GetStringBetween(htmlParser, config.Value.start_pattern, config.Value.end_pattern);
                                            if (!string.IsNullOrWhiteSpace(htmlParser))
                                            {
                                                content_text = htmlParser;
                                                the_last_config = config.Value.start_pattern;
                                            }
                                        }


                                        //################## process remove html tag ###############################
                                        try
                                        {
                                            if (!string.IsNullOrWhiteSpace(config.Value.remove_html))
                                            {
                                                if (Convert.ToInt32(config.Value.remove_html) == 1)
                                                {
                                                    if (!string.IsNullOrWhiteSpace(htmlParser))
                                                    {
                                                        content_text = HtmlUtility.RemoveAllTag(htmlParser);
                                                    }

                                                }
                                            }
                                        }
                                        catch { }
                                        //##########################################################################
                                    } //endfor

                                }

                              

                                if (!string.IsNullOrWhiteSpace(content_text))
                                {
                                    if (field_name.ToLower() == "title")
                                    {
                                        dict_post.Add("title", content_text);
                                        content_khongdau += StringHelpers.ConvertToKD(content_text) + "\n\n";
                                    }

                                    if (field_name.ToLower() == "tag_name")
                                    {
                                        if (string.IsNullOrWhiteSpace(content_text))
                                        {
                                            content_text = "Không xác định";
                                        }
                                        dict_post.Add("tag_name", content_text.Trim().Replace("&nbsp;", ""));
                                    }
                               
                                    if (field_name.ToLower() == "published_at")
                                    {
                                        bool checkDateTime = false;
                                        if (content_text.Contains("."))
                                        {
                                            content_text = content_text.Replace(".", "/");
                                        }
                                        //content_text = "Thứ hai, 15/5/2017&nbsp;|&nbsp;09:48 GMT+7";
                                        DateTime _published_date = Utility.ConvertDateTimeStringForArchived(content_text, out checkDateTime).Date;
                                        if (checkDateTime)
                                        {
                                            dict_post.Add("published_at", string.Format("{0}-{1}-{2}", _published_date.Year, _published_date.Month, _published_date.Day));
                                        }
                                        else
                                        {
                                          

                                            DateTime _published_date_ = Utility.ConvertDateTimeStringForArchived(content_text, out checkDateTime).Date;
                                            string message = string.Format("[{0}] ERROR: {1} , {2}, {3}", ThreadName, "NOT PARSING DATATIME", content_text, url);
                                            SpiderSingletonEvent.Instance.OnSpiderScreenConsole(new BlankSpider.Spider.Events.SpiderArgs() { Message = message });
                                            continue;
                                        }
                                        content_text = content_text.Trim().Replace("&nbsp;", "");
                                        dict_post.Add("published_time", content_text);
                                    }



                                    if(field_name.ToLower() == "content")
                                    {
                                        content_filter = HtmlUtility.RemoveAllTag(content_text);
                                        content_khongdau += StringHelpers.ConvertToKD(content_text) + "\n\n";
                                        dict_post.Add("content", content_text);
                                    }
                                    

                                    //_dataString.Add(field_name, content_text);

                                }

                            }


                            //_dataString.Add("html_data", html);
                            //string jsonContent = JsonConvert.SerializeObject(_dataString, Formatting.Indented);
                            //dict_post.Add("data", jsonContent);
                            dict_post.Add("content_filter", content_filter);
                            dict_post.Add("content_khongdau", content_khongdau);
                            dict_post.Add("full_html", WebUtility.HtmlEncode(html));

                            string jsonContent = JsonConvert.SerializeObject(dict_post, Formatting.Indented);

                            if (!string.IsNullOrWhiteSpace(jsonContent))
                            {
                                string results = string.Empty;
                                if (this.Mode != "TEST") // if not test then insert to api db
                                {
                                    results = DownloadExpress.DownloadPost(this.PostURL, jsonContent);
                                    if (!string.IsNullOrEmpty(results)) //check data return from api if not null or empty
                                    {
                                        try
                                        {
                                            var _info = JsonConvert.DeserializeObject<Dictionary<string, string>>(results);

                                            string type = _info["type"];
                                          


                                            switch (type)
                                            {
                                                case "UPDATE_VERSION_SUCCESS":
                                                    string content_id = _info["content_id"];
                                                    this._BaseManagement.TotalUpdateLink = this._BaseManagement.TotalUpdateLink + 1;
                                                    CounterManager.ResetTryingCount();
                                                    //--------------BEGIN capture content to images -------------------------------------------------------------------
                                          
                                                    CaptureArchivied.PDFConverterServiceClient _captureService = new CaptureArchivied.PDFConverterServiceClient();
                                                    CaptureArchivied.ConvertPDFRequest _pdfRequest = new CaptureArchivied.ConvertPDFRequest();
                                                    _pdfRequest.contentId = content_id;
                                                    _pdfRequest.pageURL = url;
                                                    _pdfRequest.filterBy = this.chk_unique_css == 1 ? 0 : 1;
                                                    _pdfRequest.filterText = this.filter_pdf;
                                                    _pdfRequest.deleteItems = this.remove_filter_pdf;
                                                    var return_data = _captureService.ConvertToPDF(_pdfRequest);

                                                    //--------------END capture content to images ---------------------------------------------------------------------
                                                    break;
                                                case "INSERT_SUCCESS":
                                                    string content_insert_id = _info["content_id"];
                                                    this._BaseManagement.TotalInsertLink = this._BaseManagement.TotalInsertLink + 1;
                                                    CounterManager.ResetTryingCount();
                                                    //--------------BEGIN capture content to images -------------------------------------------------------------------
                                          
                                                    CaptureArchivied.PDFConverterServiceClient insert_captureService = new CaptureArchivied.PDFConverterServiceClient();
                                                    CaptureArchivied.ConvertPDFRequest insert_pdfRequest = new CaptureArchivied.ConvertPDFRequest();
                                                    insert_pdfRequest.contentId = content_insert_id;
                                                    insert_pdfRequest.pageURL = url;
                                                    insert_pdfRequest.filterBy = this.chk_unique_css == 1 ? 0 : 1 ;
                                                    insert_pdfRequest.filterText = this.filter_pdf;
                                                    insert_pdfRequest.deleteItems = this.remove_filter_pdf;
                                                    var insert_return_data = insert_captureService.ConvertToPDF(insert_pdfRequest);


                                                    








                                                    //--------------END capture content to images ---------------------------------------------------------------------
                                                    break;
                                                default:
                                                    CounterManager.InCreaseTryingCount();
                                                    break;
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
                                        Title = (string)dict_post["title"],
                                        Message = results
                                    };
                                    SpiderSingletonEvent.Instance.OnSpiderProcessing(args);
                                }
                                catch { }

                            }
                        }
                        catch(Exception _errorParsing) {
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

                            string message = string.Format("[{0}] SLEEP FOR UPDATE AFTER {1} seconds, {2}", ThreadName, (this.UpdateSleep/1000), DateTime.Now);
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
                            url = this.BaseURL + objUrl.href;

                        try
                        {

                            //html = DownloadExpress.Download(url, Utility.GetEncoding(EncodingType.UTF8));
                            html = DownloadExpress.getResponseString(url,"");

                        }
                        catch(Exception _error)
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
                                html = DownloadExpress.getResponseString(url, "");
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

                            if(item.pattern_type == "XPATH")
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
                                try
                                {
                                    foreach (string linkSub in listLinkSub)
                                    {
                                        string _url = linkSub;

                                        foreach (var item_detail_link in _configLinkDetailLink) // detail link pattern
                                        {
                                            Regex rg = new Regex(item_detail_link.url_pattern);
                                            Match mt;
                                            mt = rg.Match(_url);
                                            if (mt.Success)
                                            {
                                                //if detail link add to queue detail
                                                if (IsNewUrl(ref _url))
                                                {
                                                    ConcreteLink _concreteLink = new ConcreteLink();
                                                    _concreteLink.link_type = "DETAIL_LINK";
                                                    _concreteLink.href = _url;
                                                    QueueDetailURL.Enqueue(_concreteLink);
                                                }
                                                try
                                                {
                                                    //remove detail link in list sub link
                                                    listSubLinkRemove.Add(_url);
                                                }
                                                catch { }
                                                //##################################################################
                                            }
                                        }
                                    }
                                }
                                catch { }

                                //#####################################################################

                                if(listSubLinkRemove.Count > 0)
                                {
                                    listLinkSub = listLinkSub.Where(s => !listSubLinkRemove.Contains(s)).ToList();
                                }
                                
                                foreach (string linkSub in listLinkSub)
                                {

                                    string _url = linkSub;
                                    if (IsNewUrl(ref _url))
                                    {
                                        ConcreteLink _concreteLink = new ConcreteLink();
                                        _concreteLink.link_type = "SUB_LINK";
                                        _concreteLink.href = linkSub;
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
                                CounterManager.Reset(0, 100);
                                UrlStoragClear();
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
