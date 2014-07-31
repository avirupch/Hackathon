/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.IO;
using System.Net;

using Newtonsoft.Json;  //Install Json.Net package
using Newtonsoft.Json.Linq;

using System.ServiceModel.Syndication;

namespace RssNewsReader
{
    public class RssParser_Unused
    {
        string rssFeedUrl = "";
        SyndicationFeed feed;
        Dictionary<string, string> categories = null;

        public void initialize(string rssFeedUrl)
        {
            XmlReader reader = XmlReader.Create(rssFeedUrl);
            feed = SyndicationFeed.Load(reader);
            reader.Close();
        }

        public Dictionary<string, string> getCategories()
        {
            categories = new Dictionary<string, string>();
            
            
            foreach (SyndicationItem item in feed.Items)
            {                
                String subject = item.Title.Text;    
                String summary = item.Summary.Text;
                
                categories.Add(subject, summary);
                     
            }            
            return categories;
        }        

        public List<NewsItem> getTopStories(int maxCount = 5)
        {            
            List<NewsItem> topStories = new List<NewsItem>();

            foreach (SyndicationItem item in feed.Items)
            {
                //topStories.Add(new NewsItem(item.Title.Text, item.Summary.Text));

                Console.WriteLine("Title: " + item.Title.Text);
                Console.WriteLine("Summary: " + item.Summary.Text);
                //Console.WriteLine("Content: " + item.Content.ToString());
                //Console.WriteLine("BaseUri: " + item.BaseUri.AbsoluteUri);
                Console.WriteLine("Link BaseUrl: " + item.Links[0].BaseUri);
                
                foreach (var cat in item.Categories)
                {
                    Console.WriteLine("Cat Label: " + cat.Label);
                    Console.WriteLine("Cat Name: " + cat.Name);
                    Console.WriteLine("Cat Scheme: " + cat.Scheme);                    
                }
                Console.ReadKey();

            }
            
            return topStories;
        }
        
    }
}
*/