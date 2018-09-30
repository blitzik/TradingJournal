using prjt.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace intf.Converters
{
    public class IntToMonthNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Date.Months[12 - (int)value];
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 12 - Date.Months.FindIndex(s => s == (string)value);
        }
    }
}
