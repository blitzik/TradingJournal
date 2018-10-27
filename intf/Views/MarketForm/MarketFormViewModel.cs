using Caliburn.Micro;
using Common.Commands;
using Common.Validation;
using intf.BaseViewModels;
using intf.Subscribers.Markets.Messages;
using prjt.Domain;
using prjt.Facades;
using prjt.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Views
{
    public class MarketFormViewModel : BaseScreen
    {
        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                Set(ref _symbol, value);
                SaveSignalCommand.RaiseCanExecuteChanged();
            }
        }


        private Market _market;
        public Market Market
        {
            get { return _market; }
            set
            {
                Set(ref _market, value);
                Symbol = value.Symbol;
            }
        }


        private DelegateCommand<object> _saveSignalCommand;
        public DelegateCommand<object> SaveSignalCommand
        {
            get
            {
                if (_saveSignalCommand == null) {
                    _saveSignalCommand = new DelegateCommand<object>(
                        p => SaveMarket(),
                        p => Validation.Check(nameof(Symbol), Symbol)
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


        private MarketFacade _marketFacade;
        private MarketFactory _marketFactory;
        
        public MarketFormViewModel(MarketFacade marketFacade, MarketFactory marketFactory)
        {
            _marketFacade = marketFacade;
            _marketFactory = marketFactory;
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();
        }


        protected override void InitializeValidation()
        {
            base.InitializeValidation();

            Validation.AddRuleSet(
                nameof(Symbol),
                new RuleSet<string>().AddRule(x => { return string.IsNullOrEmpty(x); }, "Please fill the \"Market symbol\" field")
                                     .AddRule(x => { return !string.IsNullOrEmpty(x) && x.Length < 3; }, "Market symbol must have at least 3 characters")
            );
        }


        public event Action<Market> OnSuccessfullySavedMarket;
        private void SaveMarket()
        {
            Market market;
            if (Market != null) {
                market = Market;
                market.Symbol = Symbol;
                _marketFacade.UpdateMarket(market);

            } else {
                market = _marketFactory.Create(Symbol);
                _marketFacade.StoreMarket(market);
            }

            EventAggregator.PublishOnUIThread(new MarketSuccessfullySavedMessage());

            OnSuccessfullySavedMarket?.Invoke(market);
        }
    }
}