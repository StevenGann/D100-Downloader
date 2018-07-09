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
            if (args.Length > 1)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    //Console.WriteLine(args[i]);
                    string html = DndSpeakClient.DownloadHtml(args[i]);
                    string title = DndSpeakClient.MakeValidFileName(DndSpeakClient.ExtractTitle(html));
                    List<string> items = DndSpeakClient.ExtractList(html);
                    string tsv = DndSpeakClient.ListToTsv(items);
                    Console.WriteLine(title);
                    DndSpeakClient.StringToFile(tsv, title + ".tsv");
                }

                Console.WriteLine("Exporting complete!");
            }
            else
            {
                Console.WriteLine("Usage:");
                Console.WriteLine("D100Downloader.exe \"url1\" \"url2\" \"url3\"... ");
                Console.WriteLine();
            }
        }
    }
}
