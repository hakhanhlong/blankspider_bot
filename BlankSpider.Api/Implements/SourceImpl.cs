using BlankSpider.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Api.Implements
{
    public class SourceImpl: ISourceImpl
    {
        public SourceImpl() { }

        public Task<string> GetAll()
        {
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() =>
            {
                string result = HttpClientHelpers.Instance().GET(URLConstants.SOURCE_GET_ALL);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

        public Task<string> GetByServerIP(string serverip)
        {
            string url = string.Format(URLConstants.SOURCE_GETBY_SERVERIP, serverip);
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() =>
            {
                string result = HttpClientHelpers.Instance().GET(url);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

        

        public Task<string> GetByID(string id)
        {
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() =>
            {
                string url = string.Format(URLConstants.SOURCE_GETBY_ID, id);
                string result = HttpClientHelpers.Instance().GET(url);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

        public Task<string> GetConfigGeneral(string id)
        {
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() =>
            {
                string url = string.Format(URLConstants.SOURCE_GET_CONFIG_GENERALS, id);
                string result = HttpClientHelpers.Instance().GET(url);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

        public Task<string> GetConfigLink(string id)
        {
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() =>
            {
                string url = string.Format(URLConstants.SOURCE_GET_CONFIG_LINKS, id);
                string result = HttpClientHelpers.Instance().GET(url);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

        public Task<string> GetConfigField(string id)
        {
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() =>
            {
                string url = string.Format(URLConstants.SOURCE_GET_CONFIG_FIELDS, id);
                string result = HttpClientHelpers.Instance().GET(url);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

        public Task<string> GetConfigVideo(string id)
        {
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() =>
            {
                string url = string.Format(URLConstants.SOURCE_GET_CONFIG_VIDEOS, id);
                string result = HttpClientHelpers.Instance().GET(url);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

    }
}
