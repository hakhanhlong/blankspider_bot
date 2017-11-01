using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Logging
{
    public interface ILogging
    {
        void Log(object sender, LoggingEventArgs e);
    }
}
