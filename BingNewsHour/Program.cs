using System;
using System.IO;
using System.Speech.Synthesis;
using Newtonsoft.Json;
using RssNewsReader;
using System.Collections.Generic;

namespace Microsoft.Hackathon.NewsReader
{
    public class Reader
    {
        static bool isLocalRssFeed = true;  //the flow for isLocalRssFeed = false is not yet supported

        public static void ReadNewsByCategory(string cat)
        {
            RssParser rssParser = new RssParser();
            string feedFileName = GetRssFeedFileNameByCategory(cat);
            if (isLocalRssFeed)
                rssParser.initializeLocal(feedFileName);
            else
                rssParser.initialize("https://www.bing.com/news/?format=RSS");

            ReadOutLoud(GetNewsSummary(rssParser));
        }

        public static string GetRssFeedFileNameByCategory(string cat)
        {
            return @"C:\Users\avirupch\Desktop\Hackathon 1407\SampleBingNewsRssFeed.txt";
        }

        public static List<NewsSummary> GetNewsSummary(RssParser rssParser)
        {
            List<NewsSummary> ListOfSummaries = new List<NewsSummary>();

            List<NewsItem>  topStories = rssParser.getTopStories(2);
            foreach (NewsItem newsItem in topStories)
            {
                ListOfSummaries.Add(new NewsSummary(newsItem.url));
            }
            
            return ListOfSummaries;
        }

        public static void ReadOutLoud(List<NewsSummary> ListOfSummaries)
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                synth.SetOutputToDefaultAudioDevice();
                synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
                // Speak a string synchronously.
                foreach (NewsSummary _summary in ListOfSummaries)
                {
                    string textToRead = _summary.title + ".\n" + _summary.source + " reports, " + _summary.digest + "\n\n";
                    try
                    {
                        synth.Speak(textToRead);
                    }
                    catch
                    {

                    }
                    Console.Write(textToRead); //*
                }
            }
        }
    }
}