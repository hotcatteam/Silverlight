using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

namespace TwitterSearch.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? date = value as DateTime?;
            
            if(culture == null)
            {
                culture = Thread.CurrentThread.CurrentUICulture;
            }

            if (date != null)
            {
                String dateTimeFormat = parameter as string;
                return "Data: " + date.Value.ToString(dateTimeFormat, culture);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
