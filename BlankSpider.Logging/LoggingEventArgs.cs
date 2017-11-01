using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Logging
{
    public class LoggingEventArgs: EventArgs
    {
        public LoggingEventArgs(LoggingServerity _loggingServerity, string _message, Exception _exception, DateTime _datetime)
        {
            Serverity = _loggingServerity;
            Message = _message;
            Exception = _exception;
            Date = _datetime;
        }

        public LoggingServerity Serverity { get; private set; }
        public string Message { get; private set; }
        public Exception Exception { get; private set; }

        public DateTime Date { get; private set; }

        public string ServerityToString
        {
            get
            {
                return Serverity.ToString("G");
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] - {1} - {2} - {3}", Date, ServerityToString, Message, Exception.ToString());
        }


    }
}
