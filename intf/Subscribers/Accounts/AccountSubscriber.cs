using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using intf.Subscribers.Accounts.Messages;
using Common.FlashMessages;

namespace intf.Subscribers
{
    public class AccountSubscriber : BaseSubscriber,
        IHandle<AccountSuccessfullyCreatedMessage>
    {
        public AccountSubscriber(IEventAggregator eventAggregator, IFlashMessagesManager flashMessagesManager) : base(eventAggregator, flashMessagesManager)
        {
        }


        public void Handle(AccountSuccessfullyCreatedMessage message)
        {
            _flashMessagesManager.DisplayFlashMessage("Account has been successfully created.", Common.FlashMessages.Type.SUCCESS);
        }
    }
}
