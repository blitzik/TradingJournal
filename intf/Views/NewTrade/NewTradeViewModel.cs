using Common.Commands;
using intf.BaseViewModels;
using intf.Utils;
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
    public class NewTradeViewModel : BaseScreen
    {
        private DateTime _openTime;
        public DateTime OpenTime
        {
            get { return _openTime; }
            set { Set(ref _openTime, value); }
        }


        private DateTime _closeTime;
        public DateTime CloseTime
        {
            get { return _closeTime; }
            set { Set(ref _closeTime, value); }
        }


        private Signal _promptSignal = new Signal() { Name = "Select a signal" };
        private ObservableCollection<Signal> _signals;
        public ObservableCollection<Signal> Signals
        {
            get { return _signals; }
        }


        private int _selectedSignal;
        public int SelectedSignal
        {
            get { return _selectedSignal; }
            set
            {
                Set(ref _selectedSignal, value);
                SaveTradeCommand.RaiseCanExecuteChanged();
            }
        }


        private Market _promptMarket = new Market() { Symbol = "Select a market" };
        private ObservableCollection<Market> _markets;
        public ObservableCollection<Market> Markets
        {
            get { return _markets; }
        }


        private int _selectedMarket;
        public int SelectedMarket
        {
            get { return _selectedMarket; }
            set
            {
                Set(ref _selectedMarket, value);
                SaveTradeCommand.RaiseCanExecuteChanged();
            }
        }


        private double? _entryPrice;
        public string EntryPrice
        {
            get { return _entryPrice.ToString(); }
            set { Set(ref _entryPrice, DoubleOnlyUtils.Convert(value, _entryPrice)); }
        }


        private double? _exitPrice;
        public string ExitPrice
        {
            get { return _exitPrice.ToString(); }
            set { Set(ref _exitPrice, DoubleOnlyUtils.Convert(value, _exitPrice)); }
        }


        private List<Direction> _directions;
        public List<Direction> Directions
        {
            get { return _directions; }
            set { Set(ref _directions, value); }
        }


        private Direction _selectedDirection;
        public Direction SelectedDirection
        {
            get { return _selectedDirection; }
            set { Set(ref _selectedDirection, value); }
        }


        private double? _tradeOpenCommission;
        public string TradeOpenCommission
        {
            get { return _tradeOpenCommission.ToString(); }
            set { Set(ref _tradeOpenCommission, DoubleOnlyUtils.Convert(value, _tradeOpenCommission)); }
        }


        private double?  _tradeCloseCommission;
        public string TradeCloseCommission
        {
            get { return _tradeCloseCommission.ToString(); }
            set { Set(ref _tradeCloseCommission, DoubleOnlyUtils.Convert(value, _tradeCloseCommission)); }
        }


        private double? _stopLoss;
        public string StopLoss
        {
            get { return _stopLoss.ToString(); }
            set { Set(ref _stopLoss, DoubleOnlyUtils.Convert(value, _stopLoss)); }
        }


        private double? _targetProfit;
        public string TargetProfit
        {
            get { return _targetProfit.ToString(); }
            set { Set(ref _targetProfit, DoubleOnlyUtils.Convert(value, _targetProfit)); }
        }


        private double? _volume;
        public string Volume
        {
            get { return _volume.ToString(); }
            set { Set(ref _volume, DoubleOnlyUtils.Convert(value, _volume)); }
        }


        private double? _spread;
        public string Spread
        {
            get { return _spread.ToString(); }
            set { Set(ref _spread, DoubleOnlyUtils.Convert(value, _spread)); }
        }


        private bool _isStopLossHit;
        public bool IsStopLossHit
        {
            get { return _isStopLossHit; }
            set { Set(ref _isStopLossHit, value); }
        }


        private bool _isTargetProfitHit;
        public bool IsTargetProfitHit
        {
            get { return _isTargetProfitHit; }
            set { Set(ref _isTargetProfitHit, value); }
        }


        private double? _profitLoss;
        public string ProfitLoss
        {
            get { return _profitLoss.ToString(); }
            set { Set(ref _profitLoss, DoubleOnlyUtils.Convert(value, _profitLoss)); }
        }


        private DelegateCommand<object> _saveTradeCommand;
        public DelegateCommand<object> SaveTradeCommand
        {
            get
            {
                if (_saveTradeCommand == null) {
                    _saveTradeCommand = new DelegateCommand<object>(
                        p => SaveTrade()/*,
                        p => SelectedSignal != 0 && SelectedMarket != 0*/
                    );
                }
                return _saveTradeCommand;
            }
        }


        private readonly SignalFacade _signalFacade;
        private readonly MarketFacade _marketFacade;
        private readonly TradeFacade _tradeFacade;

        public NewTradeViewModel(
            SignalFacade signalFacade,
            MarketFacade marketFacade,
            TradeFacade tradeFacade
        ) {
            _signalFacade = signalFacade;
            _marketFacade = marketFacade;
            _tradeFacade = tradeFacade;

            BaseWindowTitle = "New Trade";
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();

            Directions = new List<Direction>() {
                Direction.LONG,
                Direction.SHORT
            };
            SelectedDirection = Direction.LONG;

            _signals = new ObservableCollection<Signal>(_signalFacade.FindSignals());
            _signals.Insert(0, _promptSignal);
            SelectedSignal = 0;

            _markets = new ObservableCollection<Market>(_marketFacade.FindMarketSymbols());
            _markets.Insert(0, _promptMarket);
            SelectedMarket = 0;
        }


        protected override void InitializeValidation()
        {
            base.InitializeValidation();

            // todo
        }


        // -----


        private void SaveTrade()
        {
            /*Trade t = new Trade(
                Identity.Account.CurrentBalance,
                OpenTime,
                Markets[SelectedMarket],
                Signals[SelectedSignal],
                SelectedDirection,
                _volume ?? 0,
                _entryPrice ?? 0,
                _spread ?? 0,
                _tradeOpenCommission ?? 0,
                _stopLoss ?? 0,
                _targetProfit ?? 0
            );
            
            if (IsStopLossHit) {
                t.CloseByStopLoss(CloseTime, _exitPrice ?? 0, _profitLoss ?? 0, _tradeCloseCommission ?? 0);

            } else if (IsTargetProfitHit) {
                t.CloseByTargetProfit(CloseTime, _exitPrice ?? 0, _profitLoss ?? 0, _tradeCloseCommission ?? 0);

            } else {
                t.CloseTrade(CloseTime, _exitPrice ?? 0, _profitLoss ?? 0, _tradeCloseCommission ?? 0);
            }

            _tradeFacade.SaveTrade(t);*/

            ProgressViewModel vm = PrepareViewModel<ProgressViewModel>();
            Overlay.DisplayOverlay(vm, true);
            Task.Factory.StartNew(() => {
                _tradeFacade.GenerateData();
                Overlay.HideOverlay();
            });
        }
    }
}
