using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace BlankSpider.Spider.FindLinks
{
    public class FindLinkByXPath: FindLinkBase
    {
        public FindLinkByXPath(string baseUrl, string configContent, string linkRemove)
        {
            base.BaseUrl = baseUrl;
            base.ConfigContent = configContent;
            base.LinkRemove = linkRemove;
        }

        public override List<string> FindLink(string html)
        {
            if (Utility.Utility.IsStringNullOrEmpty(this._BaseUrl))
                return null;

            if (Utility.Utility.IsStringNullOrEmpty(this._ConfigContent))
                return null;

            if (Utility.Utility.IsStringNullOrEmpty(html))
                return null;

            List<string> listLink = new List<string>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            try {
                var listNodes = htmlDoc.DocumentNode.SelectNodes(this._ConfigContent);
                if (listNodes != null)
                {
                    foreach (var link in listNodes)
                    {
                        string href = link.Attributes["href"].Value;
                        if (href.Contains("javascript") || href.Contains("mailto"))
                            continue;
                        listLink.Add(href);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch {

            }
            
            

            // Remove Link by LinkRemove
            List<string> listFilter = null;
            if (Utility.Utility.IsStringNullOrEmpty(this._LinkRemove))
                listFilter = listLink;
            else
            {
                string[] arrRemoveRegex = this._LinkRemove.Split(';');
                foreach (string itmRemove in arrRemoveRegex)
                {
                    if (!string.IsNullOrWhiteSpace(itmRemove))
                    {
                        listFilter = new List<string>();
                        Regex rgRemove = new Regex(itmRemove);
                        Match mtRemove;
                        foreach (string item in listLink)
                        {
                            mtRemove = rgRemove.Match(item);
                            if (!mtRemove.Success)
                                listFilter.Add(item);
                        }
                        listLink = listFilter;
                    }

                }
            }

            if (listFilter == null || listFilter.Count == 0)
                return null;

            return listLink;
        }
    }
}
