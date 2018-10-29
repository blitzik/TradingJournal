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
        private ObservableCollection<SignalItemViewModel> _signals;
        public ObservableCollection<SignalItemViewModel> Signals
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
        

        private readonly SignalFacade _signalFacade;

        public SignalsViewModel(SignalFacade signalFacade)
        {
            _signalFacade = signalFacade;

            BaseWindowTitle = "Signals";
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();

            _signals = new ObservableCollection<SignalItemViewModel>(PrepareSignalItemViewModelCollection(_signalFacade.FindSignals()));
        }


        // -----


        private void DisplayForm()
        {
            Overlay.DisplayOverlay(PrepareSignalFormViewModel());
        }


        private SignalFormViewModel PrepareSignalFormViewModel()
        {
            SignalFormViewModel vm = GetViewModel<SignalFormViewModel>();
            vm.OnSuccessfullySavedSignal += (Signal signal) => {
                Signals.Add(PrepareSignalItemViewModel(signal));
                Overlay.HideOverlay();
            };
            vm.OnCancelClicked += () => { Overlay.HideOverlay(); };

            return vm;
        }


        private SignalItemViewModel PrepareSignalItemViewModel(Signal signal)
        {
            return PrepareViewModel(() => { return new SignalItemViewModel(signal); });
        }


        private List<SignalItemViewModel> PrepareSignalItemViewModelCollection(IReadOnlyCollection<Signal> signals)
        {
            List<SignalItemViewModel> result = new List<SignalItemViewModel>();
            foreach (Signal s in signals) {
                result.Add(PrepareSignalItemViewModel(s));
            }
            return result;
        }

    }
}
