using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Spider.Events
{
    public class SpiderArgs: EventArgs
    {
        public SpiderArgs() { }

        public string Message{get;set;}

        public int Index { get; set; }
        public string SourceName { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        
    }
}
