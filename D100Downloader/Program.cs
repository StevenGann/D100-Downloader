using System;
using System.Collections.Generic;

namespace D100Downloader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string template = System.IO.File.ReadAllText(@"template.html");
            string url = @"http://dndspeak.com/2018/06/100-royal-family-drama/";
            string html = DndSpeakClient.DownloadHtml(url);
            string title = DndSpeakClient.ExtractTitle(html);
            string subtitle = DndSpeakClient.ExtractSubtitle(html);
            List<string> items = DndSpeakClient.ExtractList(html);
            string tsv = DndSpeakClient.ListToTsv(items, title, subtitle, url);
            string newhtml = DndSpeakClient.ListToHtml(items, title, subtitle, url, template);
            Console.WriteLine(title);
            Console.WriteLine(subtitle);
            DndSpeakClient.StringToFile(tsv, DndSpeakClient.MakeValidFileName(title) + ".tsv");
            DndSpeakClient.StringToFile(newhtml, DndSpeakClient.MakeValidFileName(title) + ".html");
        }
    }
}
