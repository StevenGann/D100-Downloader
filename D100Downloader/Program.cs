using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D100Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string template = System.IO.File.ReadAllText(@"template.html");
            string html = DndSpeakClient.DownloadHtml(@"http://dndspeak.com/2018/06/100-royal-family-drama/");
            string title = DndSpeakClient.ExtractTitle(html);
            string subtitle = DndSpeakClient.ExtractSubtitle(html);
            List<string> items = DndSpeakClient.ExtractList(html);
            string tsv = DndSpeakClient.ListToTsv(items, title, subtitle);
            string newhtml = DndSpeakClient.ListToHtml(items, title, subtitle, template);
            Console.WriteLine(title);
            Console.WriteLine(subtitle);
            DndSpeakClient.StringToFile(tsv, DndSpeakClient.MakeValidFileName(title) + ".tsv");
            DndSpeakClient.StringToFile(newhtml, DndSpeakClient.MakeValidFileName(title) + ".html");
        }
    }
}
