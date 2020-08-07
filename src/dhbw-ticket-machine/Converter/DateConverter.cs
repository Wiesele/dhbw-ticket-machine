using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace dhbw_ticket_machine.Converter
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var seperates = value.ToString().Split(".");
            DateTime time;
            try
            {
                time = new DateTime(Int32.Parse(seperates[2]), Int32.Parse(seperates[1]), Int32.Parse(seperates[0]));
            }
            catch
            {
                time = DateTime.Now;
            }
            return time;
        }
    }
}
