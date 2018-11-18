using intf.BaseViewModels;
using prjt.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjt.Facades;
using System.Globalization;
using Common.ExtensionMethods;
using System.Data;
using System.Reflection;
using Common.Commands;

namespace intf.Views
{
    public class StatisticsViewModel : BaseScreen
    {
        private ObservableCollection<Stats> _stats;
        public ObservableCollection<Stats> Stats
        {
            get { return _stats; }
            set { _stats = value; }
        }


        private StatsPeriod _displayedPeriod;
        private StatsPeriod DisplayedPeriod
        {
            get { return _displayedPeriod; }
            set { Set(ref _displayedPeriod, value); }
        }


        private int _year;
        private int Year
        {
            get { return _year; }
            set { Set(ref _year, value); }
        }
        

        private int _month;
        private int Month
        {
            get { return _month; }
            set { Set(ref _month, value); }
        }


        private int _week;
        private int Week
        {
            get { return _week; }
            set { Set(ref _week, value); }
        }


        private ObservableCollection<Stats> _summaryStats;
        public ObservableCollection<Stats> SummaryStats
        {
            get { return _summaryStats; }
            set { Set(ref _summaryStats, value); }
        }


        private Stats _selectedStats;
        public Stats SelectedStats
        {
            get { return _selectedStats; }
            set
            {
                if (value.Day != 0) {
                    return;
                }
                
                DisplayedPeriod = value.Period;
                Year = value.Year;
                Month = value.Month;
                Week = value.Week;

                SummaryStats.Refill(new Stats[] { value });
                Stats.Refill(LoadStats(DisplayedPeriod, Year, Month, Week));
                ModifyColumnsVisibility(DisplayedPeriod);
            }
        }


        private IEnumerable<string> _hiddenColumns;
        public IEnumerable<string> HiddenColumns
        {
            get { return _hiddenColumns; }
            set { Set(ref _hiddenColumns, value); }
        }


        private DelegateCommand<object> _backCommand;
        public DelegateCommand<object> BackCommand
        {
            get
            {
                if (_backCommand == null) {
                    _backCommand = new DelegateCommand<object>(p => {
                        if (DisplayedPeriod == StatsPeriod.WEEK) {
                            DisplayedPeriod = StatsPeriod.MONTH;
                        } else if (DisplayedPeriod == StatsPeriod.MONTH) {
                            DisplayedPeriod = StatsPeriod.YEAR;
                        } else {
                            DisplayedPeriod = StatsPeriod.TOTAL;
                        }
                        Stats.Refill(LoadStats(DisplayedPeriod, Year, Month, Week));
                        ModifyColumnsVisibility(DisplayedPeriod);
                    });
                }
                return _backCommand;
            }
        }

        private readonly StatsFacade _statsFacade;

        public StatisticsViewModel(StatsFacade statsFacade)
        {
            _statsFacade = statsFacade;

            BaseWindowTitle = "Statistics";
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();
        }


        protected override void OnActivate()
        {
            base.OnActivate();

            if (Stats == null) {
                Stats = new ObservableCollection<Stats>(LoadStats(StatsPeriod.TOTAL, Year, Month, Week));
            } else {
                Stats.Refill(LoadStats(StatsPeriod.TOTAL, Year, Month, Week));
            }

            if (SummaryStats == null) {
                SummaryStats = new ObservableCollection<Stats>(new Stats[] { _statsFacade.GetTotalStats() });
            } else {
                SummaryStats.Refill(new Stats[] { _statsFacade.GetTotalStats() });
            }

            HiddenColumns = new string[] { "Day", "Week", "Month" };
        }


        private void ModifyColumnsVisibility(StatsPeriod period)
        {
            switch (period) {
                case StatsPeriod.YEAR:
                    HiddenColumns = new string[] { "Day", "Week" };
                    break;
                case StatsPeriod.MONTH:
                    HiddenColumns = new string[] { "Day" };
                    break;
                case StatsPeriod.WEEK:
                    HiddenColumns = null;
                    break;
                default:
                    HiddenColumns = new string[] { "Day", "Week", "Month" };
                    break;
            }
        }


        private IEnumerable<Stats> LoadStats(StatsPeriod period, int year = 0, int month = 0, int week = 0)
        {
            IEnumerable<Stats> stats;
            if (period == StatsPeriod.WEEK) {
                stats = _statsFacade.FindStats(year, month, week);
            } else if (period == StatsPeriod.MONTH) {
                stats = _statsFacade.FindStats(year, month);
            } else if (period == StatsPeriod.YEAR) {
                stats = _statsFacade.FindStats(year);
            } else {
                stats = _statsFacade.FindStats();
            }

            return stats;
        }
    }
}
