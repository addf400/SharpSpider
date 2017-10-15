using System;
using System.IO;
using System.Text;
using System.Linq;

namespace NetEaseSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            var spider = new Spider();

            var ids = File.ReadAllLines("songerlist.txt", Encoding.UTF8).Select(s => int.Parse(s.Split('\t').First().Trim().Split('=').Last())).ToArray();

            Console.WriteLine("Loading {0} songerid", ids.Length);
            spider.SetLogger(
                "log.txt", 
                "info.txt");
            spider.SongerId2AlbumsId(ids);

            spider.Close();

            Console.WriteLine("Hello World!");
        }
    }
}
