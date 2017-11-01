using BlankSpider.Api.Entities;
using BlankSpider.Spider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlankSpider.App.CustomEventArgs
{
    public class SourceArgs: EventArgs
    {
        public int Index { get; set; }
        public Source Source { get; set; }

        public ListViewGroup ListViewGroup { get; set; }

        public BaseSpiderManagement SpiderManagement { get; set; }
    }
}
