using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharpSpider.Core
{
    public class Logger
    {
        public int defaultLevel;
        public StreamWriter outStream;
        private object theLock;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logfile"> logfile: null print to screen</param>
        /// <param name="level"> 0: INFO, 1: DEBUG </param>
        public Logger(string logfile = null, int level = 0)
        {
            defaultLevel = level;
            if (logfile != null)
                outStream = new StreamWriter(logfile, true, Encoding.UTF8);
            theLock = new object();
        }

        public void Record(string info, int level = -1)
        {
            lock (theLock)
            {
                if (outStream == null)
                    Console.WriteLine(info);
                else
                {
                    outStream.WriteLine(info);
                    outStream.Flush();
                }
            }
        }

        public void Close()
        {
            if (outStream != null)
                outStream.Close();
        }
    }
}
