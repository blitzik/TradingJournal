using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Utils
{
    public static class IntegersOnlyUtils
    {
        public static int? Convert(string source, int? oldValue, bool nullable = false)
        {
            if (string.IsNullOrEmpty(source)) {
                if (nullable == true) {
                    return null;
                } else {
                    return 0;
                }
            }

            if (!int.TryParse(source, out int result)) {
                return oldValue;
            }

            return result;
        }


        public static int? ConvertOnlyPositive(string source, int? oldValue, bool nullable = false)
        {
            int? result = Convert(source, oldValue, nullable);

            return result < 0 ? oldValue : result;
        }


        public static int? ConvertOnlyNegative(string source, int? oldValue, bool nullable = false)
        {
            if (string.IsNullOrEmpty(source)) {
                if (nullable == true) {
                    return null;
                } else {
                    return 0;
                }
            }

            if (!int.TryParse(source, out int result)) {
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
