using Common.Commands;
using intf.BaseViewModels;
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



        public DashboardViewModel()
        {

        }


        protected override void OnInitialize()
        {
            base.OnInitialize();

            BaseWindowTitle = "Dashboard";
        }


        // -----


        private void NewTrade()
        {
            Overlay.DisplayOverlay(PrepareViewModel<NewTradeViewModel>());
        }
    }
}
