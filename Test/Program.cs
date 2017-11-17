using System;
using System.IO;
using System.Net;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int albumId = 18966;

            string url = String.Format("http://music.163.com/api/album/{0}?ext=true&id={0}&offset=0&total=true&limit=10", albumId);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            request.Referer = "http://music.163.com/";
            CookieContainer cookie = new CookieContainer();
            Uri uri = new Uri("http://music.163.com/");
            cookie.SetCookies(uri, "appver=1.5.0.75771;");
            request.CookieContainer = cookie;
            Console.WriteLine(request.CookieContainer.GetCookies(uri));

            var response = request.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            var html = sr.ReadToEnd();

            Console.WriteLine(html);
        }
    }
}
