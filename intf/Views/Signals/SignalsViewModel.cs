using Common.Commands;
using Common.Overlay;
using intf.BaseViewModels;
using prjt.Domain;
using prjt.Facades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Views
{
    public class SignalsViewModel : BaseScreen
    {
        private ObservableCollection<Signal> _signals;
        public ObservableCollection<Signal> Signals
        {
            get { return _signals; }
        }


        private DelegateCommand<object> _newSignalCommand;
        public DelegateCommand<object> NewSignalCommand
        {
            get
            {
                if (_newSignalCommand == null) {
                    _newSignalCommand = new DelegateCommand<object>(p => DisplayForm());
                }
                return _newSignalCommand;
            }
        }


        private SignalFacade _signalFacade;

        public SignalsViewModel(SignalFacade signalFacade)
        {
            _signalFacade = signalFacade;

            BaseWindowTitle = "Signals";
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();

            _signals = new ObservableCollection<Signal>(_signalFacade.FindSignals());
        }


        // -----


        private void DisplayForm()
        {
            SignalFormViewModel vm = GetViewModel<SignalFormViewModel>();
            vm.OnSuccessfullyCreatedSignal += (Signal signal) => {
                Signals.Add(signal);
                Overlay.HideOverlay();
            };
            vm.OnCancelClicked += () => { Overlay.HideOverlay(); };
            Overlay.DisplayOverlay(vm);
        }

    }
}
