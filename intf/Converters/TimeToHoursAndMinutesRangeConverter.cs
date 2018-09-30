using prjt.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace intf.Converters
{
    public class TimeToHoursAndMinutesRangeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values[0] == null || values[0] == DependencyProperty.UnsetValue) || (values[1] == null || values[1] == DependencyProperty.UnsetValue)) {
                return string.Empty;
            }

            Time start = (Time)values[0];
            Time end = (Time)values[1];

            if (start == 0 && end == 0) {
                return "-";
            }

            return string.Format("{0} - {1}", start.HoursAndMinutes, end.HoursAndMinutes);
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
