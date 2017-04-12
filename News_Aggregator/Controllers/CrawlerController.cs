using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using News_Aggregator.Models;
using System.Collections;


namespace News_Aggregator.Controllers
{
    public class CrawlerController : Controller
    {
        public ActionResult Index()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Pratama Agung/Documents/Visual Studio 2017/Projects/News_Aggregator/News_Aggregator/App_Data/crawl_result.txt"))
            {
                CrawlViva(file);
                CrawlAntara(file);
                CrawlTempo(file);
                CrawlDetik(file);
            }

            return View();
        }

        private void CrawlDetik(System.IO.StreamWriter file)
        {
            List<News> news = new List<News>();
            XmlDocument doc = new XmlDocument();
            doc.Load("http://rss.detik.com/index.php/detikcom");
            XmlNodeList items = doc.GetElementsByTagName("item");
            string temp;
            foreach(XmlNode itemNode in items) {
                News currnews = new News();
                currnews.Title = itemNode.SelectSingleNode("title").InnerText;
                temp = itemNode.SelectSingleNode("description").InnerText;
                currnews.Description = GetDescriptionDetik(temp);
                currnews.Picture = GetPictureLinkDetik(temp);
                currnews.PubDate = itemNode.SelectSingleNode("pubDate").InnerText;
                currnews.Url = itemNode.SelectSingleNode("link").InnerText;
                news.Add(currnews);
            }

            foreach (News curr in news)
            {
                file.WriteLine("Title: " + curr.Title);
                file.WriteLine("Description: " + curr.Description);
                file.WriteLine("URL: " + curr.Url);
                file.WriteLine("Picture: " + curr.Picture);
                file.WriteLine("PubDate: " + curr.PubDate);
                file.WriteLine();
            }
        }

        private void CrawlTempo(System.IO.StreamWriter file)
        {
            List<News> news = new List<News>();
            XmlDocument doc = new XmlDocument();
            doc.Load("https://www.tempo.co/rss/terkini");
            XmlNodeList items = doc.GetElementsByTagName("item");
            foreach (XmlNode itemNode in items)
            {
                News currnews = new News();
                currnews.Title = itemNode.SelectSingleNode("title").InnerText;
                currnews.Description = itemNode.SelectSingleNode("description").InnerText.Replace('\n', ' ');
                currnews.Picture = itemNode.SelectSingleNode("image").InnerText;
                currnews.PubDate = itemNode.SelectSingleNode("pubDate").InnerText;
                currnews.Url = itemNode.SelectSingleNode("link").InnerText;
                news.Add(currnews);
            }

            foreach (News curr in news)
            {
                file.WriteLine("Title: " + curr.Title);
                file.WriteLine("Description: " + curr.Description);
                file.WriteLine("URL: " + curr.Url);
                file.WriteLine("Picture: " + curr.Picture);
                file.WriteLine("PubDate: " + curr.PubDate);
                file.WriteLine();
            }
        }

        private void CrawlAntara(System.IO.StreamWriter file)
        {
            List<News> news = new List<News>();
            XmlDocument doc = new XmlDocument();
            doc.Load("http://www.antaranews.com/rss/terkini");
            XmlNodeList items = doc.GetElementsByTagName("item");
            string temp;
            foreach (XmlNode itemNode in items)
            {
                News currnews = new News();
                currnews.Title = itemNode.SelectSingleNode("title").InnerText;
                temp = itemNode.SelectSingleNode("description").InnerText;
                currnews.Description = GetDescriptionAntara(temp);
                currnews.Picture = GetPictureAntara(temp);
                currnews.PubDate = itemNode.SelectSingleNode("pubDate").InnerText;
                currnews.Url = itemNode.SelectSingleNode("link").InnerText;
                news.Add(currnews);
            }

            foreach (News curr in news)
            {
                file.WriteLine("Title: " + curr.Title);
                file.WriteLine("Description: " + curr.Description);
                file.WriteLine("URL: " + curr.Url);
                file.WriteLine("Picture: " + curr.Picture);
                file.WriteLine("PubDate: " + curr.PubDate);
                file.WriteLine();
            }
        }

        private void CrawlViva(System.IO.StreamWriter file)
        {
            List<News> news = new List<News>();
            XmlDocument doc = new XmlDocument();
            doc.Load("http://rss.viva.co.id/get/all");
            XmlNodeList items = doc.GetElementsByTagName("item");
            string temp;
            foreach (XmlNode itemNode in items)
            {
                News currnews = new News();
                currnews.Title = itemNode.SelectSingleNode("title").InnerText;
                temp = itemNode.SelectSingleNode("description").InnerText;
                currnews.Description = GetDescriptionAntara(temp);
                currnews.Picture = GetPictureViva(temp);
                currnews.PubDate = itemNode.SelectSingleNode("pubDate").InnerText;
                currnews.Url = itemNode.SelectSingleNode("link").InnerText;
                news.Add(currnews);
            }

            foreach (News curr in news)
            {
                file.WriteLine("Title: " + curr.Title);
                file.WriteLine("Description: " + curr.Description);
                file.WriteLine("URL: " + curr.Url);
                file.WriteLine("Picture: " + curr.Picture);
                file.WriteLine("PubDate: " + curr.PubDate);
                file.WriteLine();
            }
        }

        private string GetDescriptionDetik(string input)
        {
            int i = 0;
            string result;

            while(input[i] != '/' || input[i+1] != '>')
            {
                i++;
            }
            i += 2;
            result = input.Substring(i).Replace('\n', ' ');
            return result;
        }

        private string GetPictureLinkDetik(string input)
        {
            int i = 0;
            string result;
            int start, finish;

            while (input[i] != '"')
            {
                i++;
            }
            start = i + 1;
            i++;

            while(input[i] != '"')
            {
                i++;
            }
            finish = i - 1;
            result = input.Substring(start, finish - start + 1);
            return result;
        }

        private string GetDescriptionAntara(string input)
        {
            int i = input.IndexOf(">");
            if(i >= 0)
            {
                int start = i + 1;
                return input.Substring(start).Replace('\n', ' ');
            }
            else
            {
                return input.Replace('\n', ' ');
            }
        }

        private string GetPictureAntara(string input)
        {
            int i = input.IndexOf("src=");
            if(i >= 0)
            {
                int start = i + 5;
                int finish = input.IndexOf("align") - 3;
                return input.Substring(start, finish - start + 1);
            }
            else
            {
                return "";
            }
        }

        private string GetPictureViva(string input)
        {
            int i = input.IndexOf("src=");
            if (i >= 0)
            {
                int start = i + 5;
                int finish = input.IndexOf("align") - 3;
                return input.Substring(start, finish - start + 1);
            }
            else
            {
                return "";
            }
        }

        private int Max(int a, int b)
        {
            if(a > b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
    }
}