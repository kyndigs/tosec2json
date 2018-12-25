using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tosec2Json
{
    public class Dat
    {
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string version { get; set; }
        public string author { get; set; }
        public string email { get; set; }
        public string homepage { get; set; }
        public string url { get; set; }
        public List<Game> games { get; set; }
    }

    public class Game
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<Rom> roms { get; set; }
    }

    public class Rom
    {
        public string name { get; set; }
        public string size { get; set; }
        public string crc { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
    }
}
