using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BlankSpider.Api
{
    public class HttpClientHelpers
    {


        private static HttpClientHelpers _instance;

        public static HttpClientHelpers Instance()
        {
            if (_instance == null)
            {
                _instance = new HttpClientHelpers();
            }

            return _instance;
        }

        public HttpClientHelpers(string basePath)
        {
            BaseURLPath = basePath;
        }

        public HttpClientHelpers()
        {
            BaseURLPath = ConfigurationManager.AppSettings["URL_LOAD_CONFIG"].ToString();
        }

        public string GET(string path)
        {
            string jsonResponse = string.Empty;

            using (var client = new WebClient())
            {
                try
                {

                    // HTTP GET
                    var url = new Uri(this.BaseURLPath + path);
                    client.UseDefaultCredentials = true;
                    client.Headers.Add("Accept:application/json");                    
                    var response = client.DownloadData(url);
                    jsonResponse = Encoding.UTF8.GetString(response);
                }
                catch (WebException ex)
                {
                    // Http Error
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse wrsp = (HttpWebResponse)ex.Response;
                        var statusCode = (int)wrsp.StatusCode;
                        var msg = wrsp.StatusDescription;
                        //throw new HttpException(statusCode, msg);
                    }
                    else
                    {
                        //throw new HttpException(500, ex.Message);
                    }
                }
            }

            return jsonResponse;

        }

        public string POST(string path, NameValueCollection requestParams)
        {

            string jsonResponse = string.Empty;
            using (var client = new WebClient())
            {
                try
                {
                    client.UseDefaultCredentials = true;
                    client.Headers.Add("Content-Type:application/x-www-form-urlencoded");
                    client.Headers.Add("Accept:application/json");                    
                    var uri = new Uri(this.BaseURLPath + path);


                    var response = client.UploadValues(uri, "POST", requestParams);
                    jsonResponse = Encoding.UTF8.GetString(response);


                }
                catch (WebException ex)
                {
                    // Http Error
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse wrsp = (HttpWebResponse)ex.Response;
                        var statusCode = (int)wrsp.StatusCode;
                        var msg = wrsp.StatusDescription;
                        //throw Exception(statusCode);
                    }
                    else
                    {
                        //throw Exception(500);
                    }
                }
            }

            return jsonResponse;


        }





        public string BaseURLPath { get; set; }

    }
}
