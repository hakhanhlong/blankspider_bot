using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using BlankSpider.Spider.Utility;
using System.IO;
using System.Net.Sockets;

namespace BlankSpider.Spider.HtmlRequest
{
    public class DownloadExpress
    {
        public static string Download(string url)
        {
            return Download(url, null);
        }

        public static string Download(string url, Encoding encodingType)
        {
            if (Utility.Utility.IsStringNullOrEmpty(url))
                return Utility.Utility.InitializeString;

            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(url);
            webreq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.1) Gecko/20060111 Firefox/1.5.0.1";
            webreq.Method = "GET";
            webreq.KeepAlive = true;
            webreq.Referer = url;
            webreq.ContentType = "application/x-www-form-urlencoded";
            webreq.Timeout = 2000;
            webreq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)webreq.GetResponse();
            }
            catch
            {
                return Utility.Utility.InitializeString;
            }
            if (encodingType == null)
                encodingType = Encoding.UTF8;

            string htmlData = string.Empty;
            try
            {
                StreamReader responseStream = new StreamReader(response.GetResponseStream(), encodingType);
                htmlData = responseStream.ReadToEnd();
                response.Close();
                responseStream.Close();
            }
            catch{}
           
            return htmlData;
        }

        public static string Download(string url, string method, Encoding encodingType)
        {
            if (Utility.Utility.IsStringNullOrEmpty(url))
                return Utility.Utility.InitializeString;

            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(url);
            webreq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.1) Gecko/20060111 Firefox/1.5.0.1";
            webreq.Method = method;
            webreq.KeepAlive = true;
            webreq.Referer = url;
            webreq.ContentType = "application/x-www-form-urlencoded";
            webreq.Timeout = 2000;
            webreq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)webreq.GetResponse();
            }
            catch
            {
                return Utility.Utility.InitializeString;
            }
            if (encodingType == null)
                encodingType = Encoding.UTF8;

            string htmlData = string.Empty;
            try
            {
                StreamReader responseStream = new StreamReader(response.GetResponseStream(), encodingType);
                htmlData = responseStream.ReadToEnd();
                response.Close();
                responseStream.Close();
            }
            catch { }

            return htmlData;
        }

        public static string DownloadPost(string url, string postData)
        {
            return DownloadPost(url, postData, null);
        }

        public static String getResponseString(string url, string poststring)
        {
            HttpWebRequest httpRequest =
            (HttpWebRequest)WebRequest.Create(url);

            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.AllowAutoRedirect = true;
            httpRequest.KeepAlive = true;
            httpRequest.ProtocolVersion = HttpVersion.Version10;
            httpRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US; rv:1.8.1.14) Gecko/20080404 Firefox/2.0.0.14";
            httpRequest.Referer = url;
            httpRequest.Timeout = 120 * 1000;



            if (poststring != "")
            {
                httpRequest.Method = "POST";

                //byte[] bytedata = Encoding.UTF8.GetBytes(poststring);
                byte[] bytedata = ASCIIEncoding.ASCII.GetBytes(poststring);
                httpRequest.ContentLength = bytedata.Length;

                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(bytedata, 0, bytedata.Length);
                requestStream.Close();
            }

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();

            
            //WebHeaderCollection headers = httpWebResponse.Headers;

            //if (headers["Set-Cookie"] != null)
            //{
            //    cookieManager.addCookie(cookieManager.processCookie(headers["Set-Cookie"]));
            //}

            StringBuilder sb = new StringBuilder();
            using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append(line);
                }
            }
            return sb.ToString();
        }

        public static String getResponseString(string url, string poststring, string ContentType)
        {
            HttpWebRequest httpRequest =
            (HttpWebRequest)WebRequest.Create(url);

            httpRequest.ContentType = ContentType;

            httpRequest.AllowAutoRedirect = true;
            httpRequest.KeepAlive = true;
            httpRequest.ProtocolVersion = HttpVersion.Version10;
            httpRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US; rv:1.8.1.14) Gecko/20080404 Firefox/2.0.0.14";
            httpRequest.Referer = url;
            httpRequest.Timeout = 120 * 1000;

            httpRequest.Accept = ContentType;




            if (poststring != "")
            {
                httpRequest.Method = "POST";

                //byte[] bytedata = Encoding.UTF8.GetBytes(poststring);
                byte[] bytedata = ASCIIEncoding.ASCII.GetBytes(poststring);
                httpRequest.ContentLength = bytedata.Length;

                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(bytedata, 0, bytedata.Length);
                requestStream.Close();
            }

            StringBuilder sb = new StringBuilder();
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                }

                
            }
            catch(Exception _exError) { }
            


            //WebHeaderCollection headers = httpWebResponse.Headers;

            //if (headers["Set-Cookie"] != null)
            //{
            //    cookieManager.addCookie(cookieManager.processCookie(headers["Set-Cookie"]));
            //}

            
           
            return sb.ToString();
        }


        public static String getResponseString(string url, string poststring, out string _BaseURL)
        {
            HttpWebRequest httpRequest =
            (HttpWebRequest)WebRequest.Create(url);

            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.AllowAutoRedirect = true;
            httpRequest.KeepAlive = true;
            httpRequest.ProtocolVersion = HttpVersion.Version10;
            httpRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US; rv:1.8.1.14) Gecko/20080404 Firefox/2.0.0.14";
            httpRequest.Referer = url;
            httpRequest.Timeout = 120 * 1000;

            

            if (poststring != "")
            {
                httpRequest.Method = "POST";

                //byte[] bytedata = Encoding.UTF8.GetBytes(poststring);
                byte[] bytedata = ASCIIEncoding.ASCII.GetBytes(poststring);
                httpRequest.ContentLength = bytedata.Length;

                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(bytedata, 0, bytedata.Length);
                requestStream.Close();
            }

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();

            _BaseURL = string.Format("{0}://{1}", httpRequest.Address.Scheme, httpRequest.Address.Host);
            //WebHeaderCollection headers = httpWebResponse.Headers;

            //if (headers["Set-Cookie"] != null)
            //{
            //    cookieManager.addCookie(cookieManager.processCookie(headers["Set-Cookie"]));
            //}

            StringBuilder sb = new StringBuilder();
            using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append(line);
                }
            }
            return sb.ToString();
        }


        public static string DownloadPost(string url, string postData, Encoding encodingType)
        {
            if (Utility.Utility.IsStringNullOrEmpty(url) || Utility.Utility.IsStringNullOrEmpty(postData))
                return Utility.Utility.InitializeString;

            byte[] data = System.Text.Encoding.UTF8.GetBytes(postData);

            

            // Prepare web request...
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(url);
            webreq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.1) Gecko/20060111 Firefox/1.5.0.1";
            webreq.Method = "POST";
            webreq.Timeout = 1000 * 120;
            webreq.KeepAlive = true;
            webreq.Referer = url;
            webreq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            //webreq.ContentType = "application/x-www-form-urlencoded";
            webreq.ContentType = "application/json";
            webreq.ContentLength = data.Length;

            Stream newStream = webreq.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)webreq.GetResponse();
            }
            catch (Exception ex)
            {
                return "";
            }
            string htmlData = string.Empty;
            if (encodingType == null)
                encodingType = Encoding.UTF8;
            try
            {
                StreamReader responseStream = new StreamReader(response.GetResponseStream(), encodingType);

                htmlData = responseStream.ReadToEnd();
                response.Close();
                responseStream.Close();
            }
            catch { }
            
            return htmlData;
        }

        public static string DownloadBySocket(string url)
        {
            return DownloadBySocket(url, null);
        }

        public static string DownloadBySocket(string url, Encoding encodingType)
        {
            if (Utility.Utility.IsStringNullOrEmpty(url))
                return Utility.Utility.InitializeString;

            Uri uri = new Uri(url);
            WebRequestExpress request = null;
            request = WebRequestExpress.Create(uri, request, true);
            request.Header = request.Method + " " + uri.PathAndQuery + " HTTP/1.0\r\n" + request.Headers;
            WebResponseExpress response = request.GetResponse();

            if (encodingType == null)
                encodingType = Encoding.UTF8;
            response.EncodingType = encodingType;

            string strResponse = "";
            byte[] RecvBuffer = new byte[10240];
            int nBytes, nTotalBytes = 0;

            // loop to receive response buffer
            while ((nBytes = response.socket.Receive(RecvBuffer, 0, 10240, SocketFlags.None)) > 0)
            {
                nTotalBytes += nBytes;
                strResponse += encodingType.GetString(RecvBuffer, 0, nBytes);
                if (response.KeepAlive && nTotalBytes >= response.ContentLength && response.ContentLength > 0)
                    break;
            }
            return strResponse;
        }

        public static void DownloadFile(string urlName, string localName)
        {
            try
            {
                Uri uri = new Uri(urlName);

                WebRequestExpress request = null;
                request = WebRequestExpress.Create(uri, request, true);
                request.Header = request.Method + " " + uri.PathAndQuery + " HTTP/1.0\r\n" + request.Headers;
                WebResponseExpress response = request.GetResponse();

                response.EncodingType = Encoding.UTF8;

                byte[] RecvBuffer = new byte[1024];
                int nBytes, nTotalBytes = 0;

                // loop to receive response buffer
                while ((nBytes = response.socket.Receive(RecvBuffer, 0, 1024, SocketFlags.None)) > 0)
                {
                    nTotalBytes += nBytes;

                    // save block to end of file
                    SaveToFile(RecvBuffer, nBytes, localName);

                    if (response.KeepAlive && nTotalBytes >= response.ContentLength && response.ContentLength > 0)
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;  
            }
        }

        private static void SaveToFile(byte[] buffer, int count, string fileName)
        {
            if (fileName.Contains("/"))
                Directory.CreateDirectory(fileName.Substring(0, fileName.LastIndexOf('/')));

            FileStream f = null;
            try
            {
                f = File.Open(fileName, FileMode.Append, FileAccess.Write);
                f.Write(buffer, 0, count);
            }
            catch
            {

            }
            finally
            {
                if (f != null)
                    f.Close();
            }
        }        
    }
}
