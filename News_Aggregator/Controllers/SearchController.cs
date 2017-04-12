using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using News_Aggregator.Models;
using System.Collections;
using System.Text.RegularExpressions;

namespace News_Aggregator.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchResult(string key, string mode)
        {
            List<News> news = new List<News>();
            int i = 0;
            using (System.IO.StreamReader file = new System.IO.StreamReader(@"C:/Users/Pratama Agung/Documents/Visual Studio 2017/Projects/News_Aggregator/News_Aggregator/App_Data/crawl_result.txt"))
            {
                string temp;
                while (!file.EndOfStream)
                {
                    News currNews = new News();
                    currNews.Title = file.ReadLine();
                    currNews.Description = file.ReadLine();
                    currNews.Url = file.ReadLine();
                    currNews.Picture = file.ReadLine();
                    currNews.PubDate = file.ReadLine();
                    temp = file.ReadLine();
                    news.Add(currNews);
                    i++;
                }
                file.Close();
            }

            List<News> result = new List<News>();
            if(mode.Equals("Regex"))
            {
                result = SearchRegex(news, key);
            }

            ViewBag.Content = result;
            return View();
        }

        private List<News> SearchRegex(List<News> allNews, string key)
        {
            Regex r = new Regex(key, RegexOptions.IgnoreCase);
            List<News> result = new List<News>();
            for (int i = 0; i < allNews.Count; i++)
            {
                Match m = r.Match(allNews[i].Description);
                if (m.Success)
                {
                    result.Add(allNews[i]);
                }
            }
            return result;
        }
    }
}