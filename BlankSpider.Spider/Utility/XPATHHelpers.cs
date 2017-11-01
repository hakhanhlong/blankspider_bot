using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BlankSpider.Spider.Utility
{
    public class XPATHHelpers
    {
        public static string ParsingHTML(string html, string xpath_config)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlNodes = htmlDoc.DocumentNode.SelectNodes(xpath_config);


            string content = string.Empty;
            try
            {
                content = htmlNodes[0].InnerHtml;

            }
            catch
            {
                return string.Empty;
            }
            
            return content;
        }
    }
}
