using System;
using System.ServiceModel.Syndication;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using TwitterSearch.Model;

namespace TwitterSearch.Views
{
    public partial class Search : Page
    {
        public Search()
        {
            InitializeComponent();

            pcv = new PagedCollectionView(searchResults);
            pcv.SortDescriptions.Add(new SortDescription("PublishDate", ListSortDirection.Ascending));
            Loaded += new RoutedEventHandler(Search_Loaded);
        }

        void Search_Loaded(object sender, RoutedEventArgs e)
        {
            SearchResults.ItemsSource = pcv;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private const String SEARCH_URI = "http://search.twitter.com/search.atom?q={0}&since_id={1}";
        private string lastId = "0";
        private bool gotLatest = false;

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            WebClient proxy = new WebClient();
            proxy.OpenReadCompleted += new OpenReadCompletedEventHandler(proxy_OpenReadCompleted);

            busyIndicator.IsBusy = true;
            proxy.OpenReadAsync( new Uri(String.Format(SEARCH_URI, SearchBox.Text, lastId)) );
        }

        void proxy_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ChildWindow errorWindow = new ErrorWindow(e.Error);
                errorWindow.Show();
                busyIndicator.IsBusy = false;
                return;
            }

            //XDocument doc = XDocument.Load(e.Result);

            using (XmlReader feedReader = XmlReader.Create(e.Result))
            {
                gotLatest = false;

                SyndicationFeed syndFeed = SyndicationFeed.Load(feedReader);
                foreach (SyndicationItem syndicationItem in syndFeed.Items)
                {
                    searchResults.Add(
                        new TwitterSearchResult
                            {
                                Author = syndicationItem.Authors[0].Name, 
                                ID = GetTweetId(syndicationItem.Id),
                                PublishDate = syndicationItem.PublishDate.LocalDateTime, 
                                Tweet = syndicationItem.Title.Text,
                                Avatar = new BitmapImage(syndicationItem.Links[1].Uri)
                            });

                    gotLatest = true;
                }
                busyIndicator.IsBusy = false;
            }


        }

        private string GetTweetId(String tweetId)
        {
            String[] parts = tweetId.Split(':');

            if(!gotLatest)
            {
                lastId = parts[2];
            }

            return parts[2];
        }


        // Data
        private ObservableCollection<TwitterSearchResult> searchResults = new ObservableCollection<TwitterSearchResult>();
        private PagedCollectionView pcv;


    }
}
