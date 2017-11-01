using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Logging
{
    public class Logger
    {
        private LoggingServerity _serverity;
        private bool _isDebug;
        private bool _isInfo;
        private bool _isWarning;
        private bool _isError;
        private bool _isFatal;
        public Logger()
        {
            Serverity = LoggingServerity.ERROR;
        }


        public delegate void LoggingEvenHandler(object sender, LoggingEventArgs e);
        public LoggingEvenHandler Log;

        #region Singleton Logger
        private static readonly Logger instance = new Logger();
        public static Logger Instance
        {
            get { return instance; }
        }
        #endregion

        public LoggingServerity Serverity
        {
            get { return _serverity; }
            set {
                _serverity = value;

                int level = (int)_serverity;
                _isDebug = ((int)LoggingServerity.DEBUG) >= level ? true : false;
                _isInfo = ((int)LoggingServerity.INFO) >= level ? true : false;
                _isWarning = ((int)LoggingServerity.WARNING) >= level ? true : false;
                _isError = ((int)LoggingServerity.ERROR) >= level ? true : false;
                _isFatal = ((int)LoggingServerity.FATAL) >= level ? true : false;

            }
        }

        public void Debug(string message)
        {
            if (_isDebug)
                Debug(message, null);
        }
        public void Debug(string message, Exception ex)
        {
            if (_isDebug)
                OnLog(new LoggingEventArgs(LoggingServerity.DEBUG, message, ex, DateTime.Now));
        }


        public void Info(string message) {
            if (_isInfo)
                Info(message, null);
        }
        public void Info(string message, Exception ex)
        {
            if (_isInfo)
                OnLog(new LoggingEventArgs(LoggingServerity.INFO, message, ex, DateTime.Now));
        }


        public void Warning(string message) {
            if (_isWarning)
                Warning(message, null);
        }
        public void Warning(string message, Exception ex)
        {
            if (_isWarning)
                OnLog(new LoggingEventArgs(LoggingServerity.WARNING, message, ex, DateTime.Now));
        }


        public void Error(string message)
        {
            if (_isError)
                Error(message, null);
        }
        public void Error(string message, Exception ex)
        {
            if (_isError)
                OnLog(new LoggingEventArgs(LoggingServerity.ERROR, message, ex, DateTime.Now));
        }

        public void Fatal(string message)
        {
            if (_isFatal)
                Fatal(message, null);
        }
        public void Fatal(string message, Exception ex)
        {
            if (_isFatal)
                OnLog(new LoggingEventArgs(LoggingServerity.FATAL, message, ex, DateTime.Now));
        }


        public void OnLog(LoggingEventArgs e){
            if(Log!=null){
                Log(this, e);
            }
        }

        public void Attach(ILogging log){
            Log += log.Log;
        }

        public void Detach(ILogging log){
            Log -= log.Log;
        }


    }
}
