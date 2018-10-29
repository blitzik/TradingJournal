using Common.Commands;
using Common.Validation;
using intf.BaseViewModels;
using prjt.Domain;
using prjt.Facades;
using prjt.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using intf.Subscribers.Signals.Messages;

namespace intf.Views
{
    public class SignalFormViewModel : BaseScreen
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
                SaveSignalCommand.RaiseCanExecuteChanged();
            }
        }


        private Signal _signal;
        public Signal Signal
        {
            get { return _signal; }
            set
            {
                Set(ref _signal, value);
                Name = value.Name;
            }
        }


        private DelegateCommand<object> _saveSignalCommand;
        public DelegateCommand<object> SaveSignalCommand
        {
            get
            {
                if (_saveSignalCommand == null) {
                    _saveSignalCommand = new DelegateCommand<object>(
                        p => SaveSignal(),
                        p => Validation.Check(nameof(Name), Name)
                    );
                }
                return _saveSignalCommand;
            }
        }


        public event System.Action OnCancelClicked;
        private DelegateCommand<object> _cancelCommand;
        public DelegateCommand<object> CancelCommand
        {
            get
            {
                if (_cancelCommand == null) {
                    _cancelCommand = new DelegateCommand<object>(p => {
                        OnCancelClicked?.Invoke();
                    });
                }
                return _cancelCommand;
            }
        }


        private readonly SignalFacade _signalFacade;
        private readonly SignalFactory _signalFactory;

        public SignalFormViewModel(SignalFacade signalFacade, SignalFactory signalFactory)
        {
            _signalFacade = signalFacade;
            _signalFactory = signalFactory;
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();
        }


        protected override void InitializeValidation()
        {
            base.InitializeValidation();

            Validation.AddRuleSet(
                nameof(Name),
                new RuleSet<string>().AddRule(x => { return string.IsNullOrEmpty(x); }, "Please fill the \"Signal name\" field")
                                     .AddRule(x => { return !string.IsNullOrEmpty(x) && x.Length < 3; }, "Signal name must have at least 3 characters")
            );
        }


        public event Action<Signal> OnSuccessfullySavedSignal;
        private void SaveSignal()
        {
            Signal signal;
            if (Signal != null) {
                signal = Signal;
                signal.Name = Name;
                _signalFacade.UpdateSignal(signal);

            } else {
                signal = _signalFactory.Create(Name);
                _signalFacade.StoreSignal(signal);
            }
            
            EventAggregator.PublishOnUIThread(new SignalSuccessfullySavedMessage());

            OnSuccessfullySavedSignal?.Invoke(signal);
        }
    }
}
