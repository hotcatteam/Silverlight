using System;
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
    public class TwitterSearchResult
    {
        public string Author { get; set; }
        public string Tweet { get; set; }
        public DateTime PublishDate { get; set; }
        public string ID { get; set; }
        public ImageSource Avatar { get; set; }
    }
}
