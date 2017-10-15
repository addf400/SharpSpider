using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using HtmlAgilityPack;

using SharpSpider.Core;


namespace NetEaseSpider
{
    class Spider
    {
        Logger logger;
        Logger info;
        SharpSpider.Core.SharpSpider spider;
        public readonly string Host = "http://music.163.com";

        public Spider()
        {
            spider = new SharpSpider.Core.SharpSpider();
        }

        public void SetLogger(string loggerpath, string infopath)
        {
            logger = new Logger(loggerpath);
            info = new Logger(infopath);
        }

        void SongerId2AlbumsId(int songid)
        {
            var albumId = new List<int>();

            string url = String.Format(@"http://music.163.com/artist/album?id={0}", songid);
            while (url != null)
            {
                string page = spider.CrawlPage(url);

                var html = new HtmlDocument();
                html.LoadHtml(page);

                var albums = html.DocumentNode.SelectNodes("//a[@class='msk']");
                if (albums.Count == 0)
                    info.Record(String.Format("{0}\tNo any albums !", songid));
                foreach (var album in albums)
                {
                    var albumUrl = album.Attributes["href"].Value;
                    albumId.Add(int.Parse(albumUrl.Split('=').Last()));
                }

                var nextPages = html.DocumentNode.SelectNodes("//a[@class='zbtn znxt']");
                if (nextPages != null)
                {
                    if (nextPages.Count > 1)
                        info.Record(String.Format("{}\tToo many next pages !", songid));
                    url = Host + nextPages.First().Attributes["href"].Value;
                }
                else
                {
                    url = null;
                }
            }

            foreach (var i in albumId)
            {
                logger.Record(String.Format("{0}\t{1}", songid, i));
            }
            info.Record(String.Format("{0}\tDone", songid));

            if (albumId.Count == 0)
            {
                Console.WriteLine("Pay attention ! {0}", songid);
            }
        }

        public void SongerId2AlbumsId(int[] SongerId)
        {
            var theLock = new object();
            int counter = 0;

            Parallel.ForEach(SongerId, id => {
                try
                {
                    SongerId2AlbumsId(id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                lock (theLock)
                {
                    Console.Write("Load [{0} / {1}]\r", ++counter, SongerId.Length);
                }
            });
        }

        public void Close()
        {
            logger.Close();
            info.Close();
        }
    }
}
