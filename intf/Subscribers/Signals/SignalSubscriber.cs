using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common.FlashMessages;
using intf.Subscribers.Signals.Messages;

namespace intf.Subscribers
{
    public class SignalSubscriber : BaseSubscriber,
        IHandle<SignalSuccessfullySavedMessage>
    {
        public SignalSubscriber(IEventAggregator eventAggregator, IFlashMessagesManager flashMessagesManager) : base(eventAggregator, flashMessagesManager)
        {
        }


        public void Handle(SignalSuccessfullySavedMessage message)
        {
            _flashMessagesManager.DisplayFlashMessage("Signal's been successfully saved", Common.FlashMessages.Type.SUCCESS);
        }
    }
}
