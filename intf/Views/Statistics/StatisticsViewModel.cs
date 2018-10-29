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


        private Stats _selectedStats;
        public Stats SelectedStats
        {
            get { return _selectedStats; }
            set
            {
                Set(ref _selectedStats, value);
                Stats.Refill(LoadStats(value));
                _selectedStats = null; // we need to set null otherwise this this setter will be called indefinitely
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

            Stats = new ObservableCollection<Stats>(LoadStats(SelectedStats));
        }


        private IEnumerable<Stats> LoadStats(Stats selectedStats)
        {
            IEnumerable<Stats> stats;
            if (selectedStats != null) {
                if (selectedStats.Week != 0) {
                    stats = _statsFacade.FindStats(selectedStats.Year, selectedStats.Month, selectedStats.Week);
                } else if (selectedStats.Month != 0) {
                    stats = _statsFacade.FindStats(selectedStats.Year, selectedStats.Month);
                } else if (selectedStats.Year != 0) {
                    stats = _statsFacade.FindStats(selectedStats.Year);
                } else {
                    stats = _statsFacade.FindStats();
                }

            } else {
                stats = _statsFacade.FindStats();
            }

            return stats;
        }
    }
}
