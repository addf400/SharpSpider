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

            var ids = File.ReadAllLines("../TmpData/songerlist.txt", Encoding.UTF8).Select(s => int.Parse(s.Split('\t').First().Trim().Split('=').Last())).ToHashSet();
            var doneIds = File.ReadAllLines("../TmpData/info.txt", Encoding.UTF8);
            foreach (var line in doneIds)
            {
                var parts = line.Split('\t');
                if (!parts.Last().Equals("Done"))
                    Console.WriteLine("Error !");
                int id = int.Parse(parts.First());
                if (ids.Contains(id))
                    ids.Remove(id);
            }


            Console.WriteLine("Loading {0} songerid", ids.Count);
            spider.SetLogger(
                "../TmpData/log.txt",
                "../TmpData/info.txt");
            spider.SongerId2AlbumsId(ids.ToArray());

            spider.Close();

            Console.WriteLine("Hello World!");
        }
    }
}
