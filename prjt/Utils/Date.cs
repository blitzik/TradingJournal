using prjt.Comparers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Utils
{
    public class Date
    {
        private static readonly List<string> _months = new List<string>() {
            "Prosinec", "Listopad", "Říjen", "Září",
            "Srpen", "Červenec", "Červen", "Květen",
            "Duben", "Březen", "Únor", "Leden"
        };
        public static List<string> Months
        {
            get { return _months; }
        }


        private static readonly List<string> _daysOfWeek = new List<string>() {
            "Neděle", "Pondělí", "Úterý", "Středa", "Čtvrtek", "Pátek", "Sobota"
        };
        public static List<string> DaysOfWeek
        {
            get { return _daysOfWeek; }
        }


        public static int GetWeekNumber(int year, int month, int day, string cultureInfoName = "cs-CZ")
        {
            CultureInfo ci = new CultureInfo(cultureInfoName);
            return ci.Calendar.GetWeekOfYear(new DateTime(year, month, day), ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
        }


        public static List<int> GetLastYears(int numberOfYears)
        {
            int stopYear = DateTime.Now.Year - numberOfYears;
            List<int> list = new List<int>();
            for (int year = DateTime.Now.Year; year > stopYear; year--) {
                list.Add(year);
            }

            return list;
        }


        public static List<int> GetYears(int defaultYear, string order = "ASC")
        {
            int stopYear = DateTime.Now.Year;
            List<int> list = new List<int>();
            for (int year = defaultYear; year <= stopYear; year++) {
                list.Add(year);
            }

            switch (order.ToLower()) {
                case "asc":
                    return list;

                case "desc":
                    list.Sort(new SortIntDescending());
                    return list;

                default:
                    throw new Exception("Unknown order type");
            }
        }
    }
}
