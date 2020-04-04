using System;
using System.Globalization;

namespace WSViews.Classes
{
    public class DateManager
    {
        static public TimeSpan ParseTime(string input)
        {
            DateTime output;
            var ok = DateTime.TryParseExact(input, @"h:mm tt", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out output);
            return output.Subtract(output.Date);
        }
    }
}
