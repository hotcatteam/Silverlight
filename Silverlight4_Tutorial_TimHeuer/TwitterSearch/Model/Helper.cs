using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TwitterSearch.Model
{
    public class Helper
    {
        internal static string GetLastTweetId(string searchTerm)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(searchTerm))
                return IsolatedStorageSettings.ApplicationSettings[searchTerm].ToString();
            
            return "0";
        }

        internal static void SetLastTweetId(string searchTerm, string lastTweetId)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(searchTerm))
                IsolatedStorageSettings.ApplicationSettings[searchTerm] = lastTweetId;
            else
                IsolatedStorageSettings.ApplicationSettings.Add(searchTerm, lastTweetId);
        }

        internal static IEnumerable<String> GetSearchHistory()
        {
            return from x in IsolatedStorageSettings.ApplicationSettings select x.Key;
        }
    }
}
