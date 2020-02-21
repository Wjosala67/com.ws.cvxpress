using System;
using System.Globalization;
using Xamarin.Forms;

namespace WSViews
{
    public class StringToIntConverter : IValueConverter
    {
      

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }
    }
}
