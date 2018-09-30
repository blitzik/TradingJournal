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
    public class TimeToTimeInWordsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) {
                return "";
            }

            Time t = (Time)value;
            string result = "";
            if (t.Hours > 0) {
                result = string.Format("{0} {1} ", t.Hours, t.Hours == 1 ? "hodina" : ((t.Hours > 1 && t.Hours < 5) ? "hodiny" : "hodin"));
            }

            if (t.Minutes > 0) {
                result = string.Format("{0}{1}{2}{3}", result, t.Hours > 0 ? "a " : "", t.Minutes, t.Minutes == 1 ? " minuta" : ((t.Minutes > 1 && t.Minutes < 5) ? " minuty" : " minut"));
            }

            return string.IsNullOrEmpty(result) ? "-" : result;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
