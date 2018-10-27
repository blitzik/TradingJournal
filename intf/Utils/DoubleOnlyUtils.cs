using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Utils
{
    public static class DoubleOnlyUtils
    {
        public static double? Convert(string source, double? oldValue, bool nullable = false)
        {
            if (string.IsNullOrEmpty(source)) {
                if (nullable == true) {
                    return null;
                } else {
                    return 0;
                }
            }

            if (!double.TryParse(source, out double result)) {
                return oldValue;
            }

            return result;
        }


        public static double? ConvertOnlyPositive(string source, double? oldValue, bool nullable = false)
        {
            double? result = Convert(source, oldValue, nullable);

            return result < 0 ? oldValue : result;
        }


        public static double? ConvertOnlyNegative(string source, double? oldValue, bool nullable = false)
        {
            if (string.IsNullOrEmpty(source)) {
                if (nullable == true) {
                    return null;
                } else {
                    return 0;
                }
            }

            if (!double.TryParse(source, out double result)) {
                if (source.Length == 1 && source.Contains("-")) {
                    if (nullable == true) {
                        return null;
                    } else {
                        return 0;
                    }

                } else {
                    return oldValue;
                }
            }

            return result > 0 ? (result * (-1)) : result;
        }
    }
}
