using Common.Commands;
using intf.BaseViewModels;
using prjt.Domain;
using prjt.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Views
{
    public class DashboardViewModel : BaseScreen
    {
        private DelegateCommand<object> _newTradeCommand;
        public DelegateCommand<object> NewTradeCommand
        {
            get
            {
                if (_newTradeCommand == null) {
                    _newTradeCommand = new DelegateCommand<object>(p => NewTrade());
                }
                return _newTradeCommand;
            }
        }


        private TradeFacade _tradeFacade;

        public DashboardViewModel(TradeFacade tradeFacade)
        {
            _tradeFacade = tradeFacade;
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();

            BaseWindowTitle = "Dashboard";

            //IEnumerable<Stats> stats = _tradeFacade.LoadStats();
        }


        // -----


        private void NewTrade()
        {
            Overlay.DisplayOverlay(GetViewModel<NewTradeViewModel>());
        }
    }
}
