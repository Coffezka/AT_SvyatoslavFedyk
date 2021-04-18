using NUnit.Framework;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Practice5HtmlAgility
{
    public class AllItBooks
    {
        HtmlWeb web;
        string url = "https://allitbooks.net/";
        HtmlDocument document;

        [SetUp]
        public void Setup()
        {
            web = new HtmlWeb();
            document = web.Load(url);
        }

        [Test]
        public void downloadPdf()
        {
            HtmlNode[] nodes = document.DocumentNode.SelectNodes("//div[@class='post-thumbnail']/a").ToArray();

            List<string> link = new List<string>();
            foreach (HtmlNode item in nodes)
            {
                link.Add(item.Attributes["href"].Value);
            }

            List<string> sortedLink = new List<string>();
            foreach (var item in link)
            {
                sortedLink.Add(item.Split('/')[2].Split('-')[0]);
                Console.WriteLine(item.Split('/')[2].Split('-')[0]);
            }

            string dUrl = "download-file-";
            string eUrl = ".html";


            using (WebClient wc = new WebClient())
            {
                foreach (var item in sortedLink)
                {
                    var main = url + dUrl + item + eUrl;

                    var saveFiles =  item + ".pdf";

                    Console.WriteLine(main);
                    Console.WriteLine(saveFiles);
                    //C: \Users\Svyatoslav\Desktop\Lab5\Practice5HtmlAgility\Practice5HtmlAgility\bin\Debug\netcoreapp3.1
                    wc.DownloadFileAsync(new Uri(main), saveFiles);

                    while (wc.IsBusy)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }


        }
    }
}