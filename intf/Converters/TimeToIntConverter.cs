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
    public class TimeToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string v = (string)value;
            if (string.IsNullOrEmpty(v)) {
                v = "00:00";
            }
            return (new Time(v).TotalSeconds);
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (new Time((int)(double)value)).Text;
        }
    }
}
