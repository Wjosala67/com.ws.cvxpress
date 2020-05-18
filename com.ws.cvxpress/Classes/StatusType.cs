using System;
using System.Globalization;
using Xamarin.Forms;

namespace com.ws.cvxpress.Classes
{
    public class StatusType : IValueConverter
    {

        public ImageSource clock { get; set; }
        public ImageSource card { get; set; }
        public ImageSource baggrey { get; set; }
        public ImageSource bought { get; set; }
        public ImageSource dcheckgrey { get; set; }
        public ImageSource cancelled { get; set; }

        public StatusType()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null ;

            if (value.ToString() == "1") return clock;
            if (value.ToString() == "2") return card;
            if (value.ToString() == "3") return baggrey;
            if (value.ToString() == "4") return bought;
            if (value.ToString() == "5") return dcheckgrey;
            if (value.ToString() == "6") return cancelled;
            return clock;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
