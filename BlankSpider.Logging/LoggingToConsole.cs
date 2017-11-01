using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Logging
{
    public class LoggingToConsole: ILogging
    {
        public void Log(object sender, LoggingEventArgs e)
        {
            string message = string.Format("[ {0} ] [{1}]: {2}", e.Date.ToString(), e.ServerityToString, e.Message);
            System.Diagnostics.Debugger.Log(0, null, message);

        }
    }
}
