using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace RssNewsReader
{
    public class NewsItem
    {
        public string title { get; set; }
        public string url { get; set; }
        public string source { get; set; }
        public string description { get; set; }

        public NewsItem(string title, string url, string summaryJson)
        {
            this.title = title;
            this.url = url;
                
            try
            {
                JObject obj = JObject.Parse(summaryJson);
                this.source = obj["source"].ToString();
                try
                {
                    this.description = String.Join(" ", obj["summary"]);
                }
                catch
                {
                    this.description = String.Empty;
                }
            }
            catch
            {
                this.source = "bing.com";
                this.description = summaryJson;
            }
        }
    }
}
