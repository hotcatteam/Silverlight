using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using TwitterSearch.Model;

namespace TwitterSearch.Views
{
    public partial class History : Page
    {
        public History()
        {
            InitializeComponent();
            SearchTermHistory.SelectionChanged += new SelectionChangedEventHandler(SearchTermHistory_SelectionChanged);
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SearchTermHistory.ItemsSource = Helper.GetSearchHistory();
        }

        void SearchTermHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string searchTem = SearchTermHistory.SelectedItem.ToString();
            
            NavigationService.Navigate( new Uri(string.Format("/Search/{0}", searchTem), UriKind.Relative) );
        }
    }
}
