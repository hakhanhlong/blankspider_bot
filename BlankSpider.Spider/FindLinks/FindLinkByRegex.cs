using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlankSpider.Spider.Utility;
using System.Text.RegularExpressions;

namespace BlankSpider.Spider.FindLinks
{
    public class FindLinkByRegex: FindLinkBase
    {
        public FindLinkByRegex(string baseUrl, string configContent, string linkRemove)
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

            List<string> listLink = base.FindLink(html);

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

            List<string> listOutput = new List<string>();
            Regex rg = new Regex(this._ConfigContent);
            Match mt;
            foreach (string item in listLink)
            {
                mt = rg.Match(item);
                if (mt.Success)
                    listOutput.Add(item);
            }
            return listOutput;
        }
    }
}
