using intf.BaseViewModels;
using prjt.Domain;
using prjt.Facades;
using prjt.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ExtensionMethods;
using System.Globalization;

namespace intf.Views
{
    public class TradesViewModel : BaseScreen
    {
        private ObservableCollection<Trade> _trades;
        public ObservableCollection<Trade> Trades
        {
            get { return _trades; }
            set { Set(ref _trades, value); }
        }


        private ObservableCollection<int> _years;
        public ObservableCollection<int> Years
        {
            get { return _years; }
            set { Set(ref _years, value); }
        }


        private int _selectedYearIndex;
        public int SelectedYearIndex
        {
            get { return _selectedYearIndex; }
            set
            {
                Set(ref _selectedYearIndex, value);
                Trades.Refill(LoadTrades(Years[value], SelectedMonthIndex, SelectedDayIndex));
            }
        }


        private List<string> _months;
        public List<string> Months
        {
            get { return _months; }
        }


        private int _selectedMonthIndex;
        public int SelectedMonthIndex
        {
            get { return _selectedMonthIndex; }
            set
            {
                Set(ref _selectedMonthIndex, value);
                if (value == 0) {
                    Days.Refill(new string[] { "Select a month" });
                } else {
                    Days.Refill(PrepareDaysOfMonth(Years[SelectedYearIndex], 13 - value));
                    Days.Insert(0, "Entire month");
                }
                SelectedDayIndex = 0;
                Trades.Refill(LoadTrades(Years[SelectedYearIndex], value, SelectedDayIndex));
            }
        }


        private ObservableCollection<string> _days;
        public ObservableCollection<string> Days
        {
            get { return _days; }
            set { Set(ref _days, value); }
        }


        private int _selectedDayIndex;
        public int SelectedDayIndex
        {
            get { return _selectedDayIndex; }
            set
            {
                Set(ref _selectedDayIndex, value);
                Trades.Refill(LoadTrades(Years[SelectedYearIndex], SelectedMonthIndex, value));
            }
        }
        

        private readonly TradeFacade _tradeFacade;

        public TradesViewModel(TradeFacade tradeFacade)
        {
            _tradeFacade = tradeFacade;

            BaseWindowTitle = "Trades Overview";
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();

            Years = new ObservableCollection<int>(_tradeFacade.GetYears().Reverse());
            _selectedYearIndex = 0;

            _months = new List<string>(DateTimeFormatInfo.CurrentInfo.MonthNames);
            _months.Reverse();
            _months.Insert(0, "Entire Year");
            if (_months.Contains(string.Empty)) {
                _months.RemoveAt(_months.IndexOf(string.Empty));
            }
            _selectedMonthIndex = 0;

            Days = new ObservableCollection<string>() { "Select a month" };
            _selectedDayIndex = 0;

            Trades = new ObservableCollection<Trade>(LoadTrades(Years[SelectedYearIndex], SelectedMonthIndex, SelectedDayIndex));
        }


        private IList<string> PrepareDaysOfMonth(int year, int month)
        {
            List<string> days = new List<string>();
            int daysInMonth = DateTime.DaysInMonth(year, month);
            for (int i = daysInMonth; i >= 1; i--) {
                days.Add(i.ToString());
            }

            return days;
        }


        private IEnumerable<Trade> LoadTrades(int year, int monthIndex = 0, int dayIndex = 0)
        {
            IEnumerable<Trade> trades;
            if (monthIndex == 0) {
                trades = _tradeFacade.FindTrades(year);
            } else {
                if (dayIndex == 0) {
                    trades = _tradeFacade.FindTrades(year, Months.Count - monthIndex);
                } else {
                    trades = _tradeFacade.FindTrades(year, Months.Count - monthIndex, Days.Count - dayIndex);
                }
            }

            return trades;
        }
    }
}
