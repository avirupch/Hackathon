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
using System.Threading;

namespace RssNewsReader
{
    public class NewsSummaryViewModel
    {
        public NewsSummary summary = null;
        public void LoadSummary(string newsItemUrl)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://clipped.me/algorithm/clippedapi.php?url=" + newsItemUrl);
        //    request.BeginGetResponse(new AsyncCallback(Response_Completed), request);
        //}

        //void Response_Completed(IAsyncResult result)
        //{
        //    HttpWebRequest request = (HttpWebRequest)result.AsyncState;

            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            IAsyncResult result = request.BeginGetResponse(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            HttpWebResponse newsResponse = (HttpWebResponse)request.EndGetResponse(result);

            try
            {
                using (StreamReader newsStreamReader = new StreamReader(newsResponse.GetResponseStream()))
                {
                    string summaryJson = newsStreamReader.ReadToEnd();
                    JObject obj = JObject.Parse(summaryJson);

                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            summary = new NewsSummary()
                            {
                                title = obj["title"].ToString(),
                                source = obj["source"].ToString(),
                                digest = String.Join(" ", obj["summary"])
                            };
                        });

                }
            }

            catch
            {

            }
        }

        
    }
}
