using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Spider
{
    public class BaseManagement
    {
        private readonly object _lock = new object();
        public BaseManagement()
        {

        }

        private int NUMBER_CONTENT_NEW = 0;
        private int NUMBER_CONTENT_UPDATED = 0;

        public int TotalInsertLink
        {
            get
            {
                lock (_lock)
                {
                    return NUMBER_CONTENT_NEW;
                }
            }
            set
            {
                lock (_lock)
                {
                    NUMBER_CONTENT_NEW = value;
                }
            }
        }

        public int TotalUpdateLink
        {
            get
            {
                lock (_lock)
                {
                    return NUMBER_CONTENT_UPDATED;
                }
            }
            set
            {
                lock (_lock)
                {
                    NUMBER_CONTENT_UPDATED = value;
                }
            }
        }
    }
}
