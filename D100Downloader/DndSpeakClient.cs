﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace D100Downloader
{
    public class DndSpeakClient
    {

        public static List<string> ExtractList(string html)
        {
            List<string> result = new List<string>();

            string listStartToken = "<ol>";
            string listEndToken = "</ol>";
            string itemStartToken = "<li>";
            string itemEndToken = "</li>";

            int startIndex = html.IndexOf(listStartToken);
            int endIndex = html.IndexOf(listEndToken);

            if (startIndex == -1) { throw new Exception("Start of list not found."); }
            if (endIndex == -1) { throw new Exception("End of list not found."); }

            string rawList = html.Substring(startIndex + listStartToken.Length, (endIndex - (startIndex + listStartToken.Length)));
            rawList = Regex.Replace(rawList, @"\t|\n|\r", "");
            startIndex = -1;
            endIndex = -1;
            while (rawList.Length > 1)
            {
                startIndex = rawList.IndexOf(itemStartToken);
                endIndex = rawList.IndexOf(itemEndToken);

                if(startIndex < 0 || endIndex < 0) { break; }

                string item = rawList.Substring(startIndex + itemStartToken.Length, (endIndex - (startIndex + itemStartToken.Length)));
                //item = Regex.Replace(item, @"\t|\n|\r", "");
                result.Add(item);
                rawList = rawList.Remove(startIndex, (endIndex - startIndex) + itemEndToken.Length);
            }

            return result;
        }

        public static string ExtractTitle(string html)
        {
            //Console.Write(html);

            string titleStartToken = "itemprop=\"headline\">";
            string titleEndToken = "</span>";

            int startIndex = html.IndexOf(titleStartToken);
            int endIndex = html.IndexOf(titleEndToken, startIndex);

            if (startIndex == -1) { throw new Exception("Start of title not found."); }
            if (endIndex == -1) { throw new Exception("End of title not found."); }

            return html.Substring(startIndex + titleStartToken.Length, (endIndex - (startIndex + titleStartToken.Length)));
        }

        public static string ListToTsv(List<string> items)
        {
            string result = "";
            int counter = 1; //Start at 1 because dice can't roll 0
            foreach (string s in items)
            {
                result += Convert.ToString(counter) +"\t"+s+"\n";
                counter += 1;
            }

            return result;
        }

        public static void StringToFile(string data, string path)
        {
            File.WriteAllText(path, data, Encoding.UTF8);
        }

        public static string DownloadHtml(string address)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            //HttpUtility.HtmlDecode(
            string reply = HttpUtility.HtmlDecode(client.DownloadString(address));
            
            //Console.WriteLine(reply);
            return reply;
        }

        public static string MakeValidFileName(string name)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
        }
    }
}