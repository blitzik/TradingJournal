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
    public class SignalItemViewModel : BaseScreen
    {
        private Signal _signal;
        public Signal Signal
        {
            get { return _signal; }
            private set { Set(ref _signal, value); }
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


        public SignalItemViewModel(Signal signal)
        {
            Signal = signal;
        }


        private void DisplayEditForm()
        {
            SignalFormViewModel vm = GetViewModel<SignalFormViewModel>();
            vm.Signal = Signal;
            vm.OnSuccessfullySavedSignal += (Signal signal) => {
                Signal = signal;
                Overlay.HideOverlay();
            };
            vm.OnCancelClicked += () => { Overlay.HideOverlay(); };

            Overlay.DisplayOverlay(vm);
        }


        private void DisplayConfirmationDialog()
        {
            ConfirmationViewModel cvm = PrepareViewModel<ConfirmationViewModel>();
            cvm.Text = "Do you really wish to delete selected Signal?";
            cvm.OnCancelClicked += () => { Overlay.HideOverlay(); };
            cvm.OnYesClicked += () => {

            };
            
            Overlay.DisplayOverlay(cvm);
        }
    }
}
