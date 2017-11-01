using BlankSpider.Spider.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Spider.Events
{
    public class SpiderManagementArgs: EventArgs
    {
        public SpiderManagementArgs() { }

        public SOURCE_STATUS SourceStatus { get; set; }

        public string Mode { get; set; }
        public int TotalSubLink { get; set; }
        public int TotalDetailLink { get; set; }

        public int TotalRecord { get; set; }
        public int NewRecord { get; set; }

        public string Message { get; set; }
    }
}
