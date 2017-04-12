using System;
using System.Data.Entity;

namespace News_Aggregator.Models
{
    public class News
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Picture { get; set; }
        public string PubDate { get; set; }
        public string Description {get; set;}
    }

    public class NewsDBContext : DbContext {
        public DbSet<News> AllNews { get; set; }
    }
}