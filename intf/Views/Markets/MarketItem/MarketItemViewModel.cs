using Common.Commands;
using intf.BaseViewModels;
using prjt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Views
{
    public class MarketItemViewModel : BaseScreen
    {
        private Market _market;
        public Market Market
        {
            get { return _market; }
            private set { Set(ref _market, value); }
        }


        private DelegateCommand<object> _editCommand;
        public DelegateCommand<object> EditCommand
        {
            get
            {
                if (_editCommand == null) {
                    _editCommand = new DelegateCommand<object>(p => DisplayEditForm());
                }
                return _editCommand;
            }
        }


        private DelegateCommand<object> _deleteCommand;
        public DelegateCommand<object> DeleteCommand
        {
            get
            {
                if (_deleteCommand == null) {
                    _deleteCommand = new DelegateCommand<object>(p => DisplayConfirmationDialog());
                }
                return _deleteCommand;
            }
        }


        public MarketItemViewModel(Market market)
        {
            _market = market;
        }


        private void DisplayEditForm()
        {
            MarketFormViewModel vm = GetViewModel<MarketFormViewModel>();
            vm.Market = Market;
            vm.OnSuccessfullySavedMarket += (Market market) => {
                Market = market;
                Overlay.HideOverlay();
            };
            vm.OnCancelClicked += () => { Overlay.HideOverlay(); };

            Overlay.DisplayOverlay(vm);
        }


        private void DisplayConfirmationDialog()
        {
            ConfirmationViewModel cvm = PrepareViewModel<ConfirmationViewModel>();
            cvm.Text = "Do you really wish to delete selected Market?";
            cvm.OnCancelClicked += () => { Overlay.HideOverlay(); };
            cvm.OnYesClicked += () => {

            };

            Overlay.DisplayOverlay(cvm);
        }
    }
}
