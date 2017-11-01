using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace BlankSpider.Spider.HtmlRequest
{
    class WebRequestExpress
    {
        public WebRequestExpress(Uri uri, bool bKeepAlive)
        {
            Headers = new WebHeaderCollection();
            RequestUri = uri;
            Headers["Host"] = uri.Host;
            KeepAlive = bKeepAlive;
            if (KeepAlive)
                Headers["Connection"] = "Keep-Alive";
            Method = "GET";
        }
        public static WebRequestExpress Create(Uri uri, WebRequestExpress AliveRequest, bool bKeepAlive)
        {
            if (bKeepAlive &&
                AliveRequest != null &&
                AliveRequest.response != null &&
                AliveRequest.response.KeepAlive &&
                AliveRequest.response.socket.Connected &&
                AliveRequest.RequestUri.Host == uri.Host)
            {
                AliveRequest.RequestUri = uri;
                return AliveRequest;
            }
            return new WebRequestExpress(uri, bKeepAlive);
        }
        public WebResponseExpress GetResponse()
        {
            if (response == null || response.socket == null || response.socket.Connected == false)
            {
                response = new WebResponseExpress();
                response.Connect(this);
                response.SetTimeout(Timeout);
            }
            response.SendRequest(this);
            response.ReceiveHeader();
            return response;
        }

        public int Timeout = 120 * 1000;
        public WebHeaderCollection Headers;
        public string Header;
        public Uri RequestUri;
        public string Method;
        public WebResponseExpress response;
        public bool KeepAlive;
    }
}
