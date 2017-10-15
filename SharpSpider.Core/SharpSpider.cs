using System;
using System.IO;
using System.Net;
using System.Text;

namespace SharpSpider.Core
{
    public class SharpSpider
    {
        public readonly string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";

        public SharpSpider()
        {
            ServicePointManager.DefaultConnectionLimit = 100;
        }

        public string CrawlPage(string url, WebProxy proxy = null)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.UserAgent = this.UserAgent;
            if (proxy != null)
                request.Proxy = proxy;

            var response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            return sr.ReadToEnd();
        }
    }
}
