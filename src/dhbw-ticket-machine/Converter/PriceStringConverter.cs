using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace dhbw_ticket_machine.Converter
{
    public class PriceStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return float.Parse(value.ToString()).ToString("0.00").Replace(".",",") + " €";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return float.Parse(value.ToString().Replace("€", ""));
        }
    }
}
