using BlankSpider.Api.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Api.Implements
{
    public class ProjectImpl: IProjectImpl
    {
        public ProjectImpl() { }
        public Task<string> GetAll()
        {
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() => {
                string result = HttpClientHelpers.Instance().GET(URLConstants.PROJECT_GET_ALL);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

        public T GetAll<T>()
        {
            string results = GetAll().Result;
            return JsonConvert.DeserializeObject<T>(results);
        }

        public Task<string> GetByID(string id)
        {
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() =>
            {
                string url = string.Format(URLConstants.PROJECT_GETBY_ID, id);
                string result = HttpClientHelpers.Instance().GET(url);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

        public Task<string> GetByServerIP(string serverip)
        {
            TaskCompletionSource<string> _taskComplete = new TaskCompletionSource<string>();
            Task.Run(() =>
            {
                string url = string.Format(URLConstants.PROJECT_GETBY_SERVERIP, serverip);
                string result = HttpClientHelpers.Instance().GET(url);
                _taskComplete.SetResult(result);
            });

            return _taskComplete.Task;
        }

        public T GetByServerIP<T>(string serverip)
        {
            string results = GetByServerIP(serverip).Result;
            return JsonConvert.DeserializeObject<T>(results);
        }
    }
}
