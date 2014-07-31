using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.IO;
using System.Net;

using Newtonsoft.Json;  //Install Json.Net package
using Newtonsoft.Json.Linq;

//using System.ServiceModel.Syndication;

namespace RssNewsReader
{
    public class RssParser
    {
        string rssFeedUrl = "";
        XDocument xmlDoc = new XDocument();
        Dictionary<string, string> categories = null;
        
        public void initializeLocal(Stream _stream)
        {
            //this.rssFeedUrl = rssFeedFile;
            try
            {
                xmlDoc = XDocument.Load(_stream);
                //xmlDoc.Load(@"D:\\Hackathon - News Reader\\SampleBingNewsRssFeed.txt");
            }
            catch (Exception e)
            {
                //Console.WriteLine("Exception : " + e.ToString());
            }
            //newsNamespace = new XmlNamespaceManager(xmlDoc.NameTable);
            //newsNamespace.AddNamespace("newsNamespace", xmlDoc.SelectSingleNode("rss").GetNamespaceOfPrefix("News"));
        }

        public void initialize(string rssFeedUrl)
        {
        //    this.rssFeedUrl = rssFeedUrl;
        //    //WebRequest.Create("https://www.bing.com/?setmkt=en-US&setlang=en&uid=6702150&FORM=NFTOUS").GetResponse();

        //    HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(rssFeedUrl);

        //    WebResponse webRes = WebReq.GetResponse();
        //    Stream rssStream = webRes.GetResponseStream();

        //    xmlDoc = new XmlDocument();
        //    try
        //    {
        //        xmlDoc.Load(rssStream);
        //        //xmlDoc.Load(@"D:\\Hackathon - News Reader\\SampleBingNewsRssFeed.txt");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Exception : " + e.ToString());
        //    }

        //    newsNamespace = new XmlNamespaceManager(xmlDoc.NameTable);           
        //    newsNamespace.AddNamespace("newsNamespace", xmlDoc.SelectSingleNode("rss").GetNamespaceOfPrefix("News"));
        }        

        //public Dictionary<string,string> getCategories()
        //{
        //    if (categories == null)
        //    {
        //        categories = new Dictionary<string, string>();
             
        //        XmlNodeList xmlNodeList = xmlDoc.SelectNodes("rss/channel/newsNamespace:CategoryItem",newsNamespace);
                
        //        for (int i = 0; i < xmlNodeList.Count; i++)
        //        {
        //            string name = xmlNodeList.Item(i).SelectSingleNode("newsNamespace:Name", newsNamespace).InnerText;
        //            string url = xmlNodeList.Item(i).SelectSingleNode("newsNamespace:Url", newsNamespace).InnerText;                    
                    
        //            categories.Add(name, url.Substring(0,url.IndexOf("RSS")+3));                            
        //        }                

        //    }
        //    return categories;
        //}

        public List<NewsItem> getTopStories(int maxCount = 5)
        {
            List<NewsItem> topStories = new List<NewsItem>();
            XNamespace newsNs = "News";
            var items = from query in xmlDoc.Descendants("item")
                       select new NewsItem
                       (
                           (string)query.Element("title"),
                           (string)query.Element("link"),
                           (string)query.Element("description")
                       );

            int index = 0;
            foreach(var item in items)
            {
                if (++index > maxCount)
                    break;
                topStories.Add((NewsItem)item);
            }
            //XmlNodeList xmlNodeList = xmlDoc.SelectNodes("rss/channel/item");
            //List<NewsItem> topStories = new List<NewsItem>();

            //for (int i = 0; i < xmlNodeList.Count; i++)
            //{
            //    if (i == maxCount)
            //        break;

            //    XmlNode titleNode = xmlNodeList.Item(i).SelectSingleNode("title");
            //    XmlNode linkNode = xmlNodeList.Item(i).SelectSingleNode("link");
            //    XmlNode dateNode = xmlNodeList.Item(i).SelectSingleNode("pubDate");
            //    XmlNode sourceNode = xmlNodeList.Item(i).SelectSingleNode("newsNamespace:Source", newsNamespace);                
            //    XmlNode categoryNode = xmlNodeList.Item(i).SelectSingleNode("newsNamespace:Category", newsNamespace);
                
            //    string title = (titleNode == null ? "" : titleNode.InnerText);
            //    string link = (linkNode == null ? "" : linkNode.InnerText);
            //    string date = (dateNode == null ? "" : dateNode.InnerText);
            //    string source = (sourceNode == null ? "" : sourceNode.InnerText);
            //    string category = (categoryNode == null ? "" : categoryNode.InnerText);

            //    topStories.Add(new NewsItem(title,link,date,source,category));                                   
            //}
            return topStories;
        }
        
    }
}
