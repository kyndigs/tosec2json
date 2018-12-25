using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tosec2Json
{
    public static class Conversion
    {

        public static Dat LoadJsonDat(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                var json = System.IO.File.ReadAllText(filename);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Dat>(json);
            }

            return null;
        }

        public static Dat LoadXmlDat(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(System.IO.File.ReadAllText(filename));

                var header = doc.GetElementsByTagName("header");

                var dat = new Dat();
                dat.name = header[0].SelectSingleNode("name")?.InnerText ?? "";
                dat.description = header[0].SelectSingleNode("description")?.InnerText ?? "";
                dat.category = header[0].SelectSingleNode("category")?.InnerText ?? "";
                dat.version = header[0].SelectSingleNode("version")?.InnerText ?? "";
                dat.author = header[0].SelectSingleNode("author")?.InnerText ?? "";
                dat.homepage = header[0].SelectSingleNode("homepage")?.InnerText ?? "";
                dat.url = header[0].SelectSingleNode("url")?.InnerText ?? "";

                var games = doc.GetElementsByTagName("game");

                dat.games = new List<Game>();

                foreach (XmlNode game in games)
                {
                    var ng = new Game();
                    ng.name = game.Attributes["name"]?.InnerText ?? "";
                    ng.description = game.SelectSingleNode("description")?.InnerText ?? "";

                    ng.roms = new List<Rom>();

                    var roms = game.SelectNodes("rom");

                    foreach (XmlNode rom in roms)
                    {
                        var nr = new Rom();
                        nr.name = rom.Attributes["name"]?.InnerText ?? "";
                        nr.size = rom.Attributes["size"]?.InnerText ?? "";
                        nr.crc = rom.Attributes["crc"]?.InnerText ?? "";
                        nr.md5 = rom.Attributes["md5"]?.InnerText ?? "";
                        nr.sha1 = rom.Attributes["sha1"]?.InnerText ?? "";

                        ng.roms.Add(nr);
                    }


                    dat.games.Add(ng);
                }

                return dat;
            }

            return null;
        }

        public static string ConvertToJson(string file)
        {
            var dat = LoadXmlDat(file);

            if (dat != null)
            {
                return JsonConvert.SerializeObject(dat);
            }

            return string.Empty;
        }
    }
}
