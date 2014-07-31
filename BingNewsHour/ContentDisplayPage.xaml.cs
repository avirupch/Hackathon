using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RssNewsReader;
using Windows.Phone.Speech.Synthesis;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace BingNewsHour
{
    public partial class ContentDisplayPage : PhoneApplicationPage
    {
        public bool pauseflag = true;
        string[] categoryIds;
        public SpeechSynthesizer synth;
        public NewsSummaryViewModel _viewModel = new NewsSummaryViewModel();
        public ContentDisplayPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ContentDisplayPage_Loaded);
        }

        void ContentDisplayPage_Loaded(object sender, RoutedEventArgs e)
        {
            ReadOutLoud();
        }

        public async void ReadOutLoud()
        {
            IEnumerable<VoiceInformation> voices = from voice in InstalledVoices.All
                                                   where voice.Language == "en-US" && voice.Gender == VoiceGender.Female
                                                   select voice;

            // Set the voice as identified by the query.
            //try
            //{
            //    if(voices.Count() > 0)
            //        synth.SetVoice(voices.ElementAt(0));
            //}
            //catch
            //{

            //}

            ICategoryRepository categoryRepository = new XmlCategoryRepository();

            foreach (string id in categoryIds)
            {
                CategoryData category = categoryRepository.GetCategoryById(Convert.ToInt32(id));
                //this.CategoryName.Text = category.Name;
                Uri imageUri = new Uri(category.Image, UriKind.Relative);
                var bitmap = new BitmapImage(imageUri);
                this.CategoryImage.Source = bitmap;
                RssParser rssParser = new RssParser();
                Uri feedUri = new Uri(category.Feed, UriKind.Relative);
                StreamResourceInfo sri = Application.GetResourceStream(feedUri);
                rssParser.initializeLocal(sri.Stream);
                List<NewsItem> topStories = rssParser.getTopStories();
                foreach (NewsItem newsItem in topStories)
                {
                    try
                    {
                        synth = new SpeechSynthesizer();
                        if (voices.Count() > 0)
                            synth.SetVoice(voices.ElementAt(0));
                        if (String.IsNullOrEmpty(newsItem.description))
                            continue;
                        this.CategoryName.Text = newsItem.title;
                        this.SummaryBlock.Text = newsItem.description;
                        string textToRead = newsItem.title + ".\n" + newsItem.source + " reports, " + newsItem.description + "\n\n";
                        await synth.SpeakTextAsync(textToRead);
                        synth.Dispose();
                    }
                    catch
                    {

                    }
                }
            }
        }

        public async void ReadNewsSummary(string url)
        {
            _viewModel.LoadSummary(url);
            NewsSummary _summary = _viewModel.summary;
            try
            {
                this.SummaryBlock.Text = _summary.digest;
                string textToRead = _summary.title + ".\n" + _summary.source + " reports, " + _summary.digest + "\n\n";
                await synth.SpeakTextAsync(textToRead);
            }
            catch
            {

            }
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            categoryIds = NavigationContext.QueryString["ids"].Split(',');

        }

        private void Button_Skip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                synth.Dispose();
            }
            catch
            { }
        }

        private void Button_Pause_Click(object sender, RoutedEventArgs e)
        {
            //if Name = Pause
            //Pause the current article and Change the name to Resume
            
            if (pauseflag)
            {
                Button _myButton = (Button)sender;
                _myButton.Content = "Resume";
                pauseflag = false;
            }
            else
            {
                Button _myButton = (Button)sender;
                _myButton.Content = "Pause";
                pauseflag = true;
            }

            //Else if Button Name = Resume
            //Resume the current article and Change the name to Pause

        }
    }
}