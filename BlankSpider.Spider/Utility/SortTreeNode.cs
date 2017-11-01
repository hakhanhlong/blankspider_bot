using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlankSpider.Spider.Utility
{
    public class SortTreeNode
    {
        public SortTreeNode Parent;
        public SortTreeNode Small;
        public SortTreeNode Great;
        public string Text;
        public int Count;
        public int ID;

        public object Tag;
    }
}
