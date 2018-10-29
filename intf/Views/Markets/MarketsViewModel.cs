using Common.Commands;
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
    public class MarketsViewModel : BaseScreen
    {
        private ObservableCollection<MarketItemViewModel> _markets;
        public ObservableCollection<MarketItemViewModel> Markets
        {
            get { return _markets; }
        }


        private DelegateCommand<object> _newMarketSymbolCommand;
        public DelegateCommand<object> NewMarketSymbolCommand
        {
            get
            {
                if (_newMarketSymbolCommand == null) {
                    _newMarketSymbolCommand = new DelegateCommand<object>(p => DisplayForm());
                }
                return _newMarketSymbolCommand;
            }
        }


        private readonly MarketFacade _marketFacade;

        public MarketsViewModel(MarketFacade marketFacade)
        {
            _marketFacade = marketFacade;

            BaseWindowTitle = "Markets";
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();

            _markets = new ObservableCollection<MarketItemViewModel>(PrepareMarketItemViewModelCollection(_marketFacade.FindMarketSymbols()));
        }


        // -----


        private void DisplayForm()
        {
            Overlay.DisplayOverlay(PrepareMarketFormViewModel());
        }


        private MarketFormViewModel PrepareMarketFormViewModel()
        {
            MarketFormViewModel vm = GetViewModel<MarketFormViewModel>();
            vm.OnSuccessfullySavedMarket += (Market market) => {
                Markets.Add(PrepareMarketItemViewModel(market));
                Overlay.HideOverlay();
            };
            vm.OnCancelClicked += () => { Overlay.HideOverlay(); };

            return vm;
        }


        private MarketItemViewModel PrepareMarketItemViewModel(Market market)
        {
            return PrepareViewModel(() => { return new MarketItemViewModel(market); });
        }


        private List<MarketItemViewModel> PrepareMarketItemViewModelCollection(IReadOnlyCollection<Market> markets)
        {
            List<MarketItemViewModel> result = new List<MarketItemViewModel>();
            foreach (Market m in markets) {
                result.Add(PrepareMarketItemViewModel(m));
            }
            return result;
        }
    }
}
