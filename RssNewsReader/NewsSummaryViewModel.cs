using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows;

namespace RssNewsReader
{
    public class NewsSummaryViewModel
    {
        public void LoadSummary(string newsItemUrl)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://clipped.me/algorithm/clippedapi.php?url=" + newsItemUrl);
            request.BeginGetResponse(new AsyncCallback(Response_Completed), request);
        }

        void Response_Completed(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse newsResponse = (HttpWebResponse)request.EndGetResponse(result);

            using (StreamReader newsStreamReader = new StreamReader(newsResponse.GetResponseStream()))
            {
                string summaryJson = newsStreamReader.ReadToEnd();
                JObject obj = JObject.Parse(summaryJson);

                
                string _title = obj["title"].ToString();
                string _digest = "";
                foreach (string str in obj["summary"])
                    _digest += str + " ";

                string _source = obj["source"].ToString();
            }
        }

        
    }
}
