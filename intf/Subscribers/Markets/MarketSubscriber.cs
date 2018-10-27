using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common.FlashMessages;
using intf.Subscribers.Markets.Messages;

namespace intf.Subscribers
{
    public class MarketSubscriber : BaseSubscriber,
        IHandle<MarketSuccessfullySavedMessage>
    {
        public MarketSubscriber(IEventAggregator eventAggregator, IFlashMessagesManager flashMessagesManager) : base(eventAggregator, flashMessagesManager)
        {
        }


        public void Handle(MarketSuccessfullySavedMessage message)
        {
            _flashMessagesManager.DisplayFlashMessage("Market's been successfully saved", Common.FlashMessages.Type.SUCCESS);
        }
    }
}
