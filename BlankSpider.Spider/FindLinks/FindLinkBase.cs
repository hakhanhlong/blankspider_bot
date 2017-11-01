using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlankSpider.Spider.Utility;

namespace BlankSpider.Spider.FindLinks
{
    public class FindLinkBase
    {
        #region Members
        protected string _ConfigContent;
        protected string _LinkRemove;
        protected string _BaseUrl;
        #endregion

        #region Constructor
        public FindLinkBase()
        {
            
        }

        public FindLinkBase(string baseUrl)
        {
            this._BaseUrl = baseUrl;
            this._ConfigContent = "";
            this._LinkRemove = "";
        }

        public FindLinkBase(string baseUrl, string configContent, string linkRemove)
        {
            this._BaseUrl = baseUrl;
            this._ConfigContent = configContent;
            this._LinkRemove = linkRemove;
        }
        #endregion

        #region Properties
        public string ConfigContent
        {
            get { return _ConfigContent; }
            set { _ConfigContent = value; }
        }

        public string LinkRemove
        {
            get { return _LinkRemove; }
            set { _LinkRemove = value; }
        }

        public string BaseUrl
        {
            get { return _BaseUrl; }
            set { _BaseUrl = value; }
        }
        #endregion

        public virtual List<string> FindLink(string html)
        {
            if (Utility.Utility.IsStringNullOrEmpty(this._BaseUrl))
                return null;

            if (Utility.Utility.IsStringNullOrEmpty(html))
                return null;

            // String Regex
            string strRef = @"(href|HREF)[ ]*=.*?[^""'#>]+[""'>]";
            html = html.Replace("href = ", "href=");
            // Regex Html by string Regex
            List<string> listUrl = RegexUtility.RegexGetListAll(ref html, strRef);
            if (listUrl != null && listUrl.Count > 0)
            {
                List<string> listLink = new List<string>();
                foreach (string url in listUrl)
                {
                    // Get Link by Regex
                    strRef = url.Substring(url.IndexOf('=') + 1).Trim('"', '\'', '#', ' ', '>');

                    if (strRef.Contains("javascript") || strRef.Contains("mailto"))
                        continue;

                    // Standard Link
                    if (strRef.ToLower().Trim() == "//" || strRef.Contains("file://"))
                        continue;

                    if (strRef.IndexOf("..") != -1 || strRef.StartsWith("/") == true || strRef.StartsWith("http://") == false)
                    {
                        if (this._BaseUrl.EndsWith("index.aspx?"))
                        {
                            strRef = "index.aspx?" + strRef;
                            strRef = strRef.Replace("??", "?");
                        }
                        try
                        {
                            strRef = new Uri(new Uri(this._BaseUrl), strRef).AbsoluteUri;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    Utility.Utility.Normalize(ref strRef);

                    // Check Link
                    string[] ExtArray = { ".gif", ".jpg", ".css", ".zip", ".exe", ".ico", ".js" };
                    bool found = true;
                    foreach (string ext in ExtArray)
                        if (strRef.ToLower().EndsWith(ext))
                        {
                            found = false;
                            break;
                        }

                    if (found == true && !listLink.Contains(strRef))
                        listLink.Add(strRef);
                }
                return listLink;
            }
            return null;
        }
    }
}
