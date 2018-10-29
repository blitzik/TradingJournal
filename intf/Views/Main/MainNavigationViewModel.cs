using Caliburn.Micro;
using Common.Commands;
using intf.BaseViewModels;
using intf.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Views
{
    public class MainNavigationViewModel : BaseScreen
    {
        private IViewModel _activeItem;
        public IViewModel ActiveItem
        {
            get { return _activeItem; }
            set
            {
                Set(ref _activeItem, value);
                
                if (value.SecondNavigation != null) {
                    SecondNavigation = value.SecondNavigation;
                    SecondNavigation.CurrentlyActivatedItem = value;
                    IsSecondNavigationActive = true;

                } else {
                    IsSecondNavigationActive = false;
                }
            }
        }


        public MainNavigationViewModel()
        {
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();

            EventAggregator.Subscribe(this);

            // DisplayListingsOverview();
        }


        public void DisplayDashboard()
        {
            EventAggregator.PublishOnUIThread(new ChangeViewMessage<DashboardViewModel>());
        }


        public void DisplayTrades()
        {
            EventAggregator.PublishOnUIThread(new ChangeViewMessage<TradesViewModel>());
        }


        public void DisplayStats()
        {
            EventAggregator.PublishOnUIThread(new ChangeViewMessage<StatisticsViewModel>());
        }


        public void DisplayMarkets()
        {
            EventAggregator.PublishOnUIThread(new ChangeViewMessage<MarketsViewModel>());
        }


        public void DisplaySignals()
        {
            EventAggregator.PublishOnUIThread(new ChangeViewMessage<SignalsViewModel>());
        }
    }
}
