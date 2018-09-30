using Caliburn.Micro;
using Common.FlashMessages;
using Common.Utils.ResultObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Subscribers
{
    public abstract class BaseSubscriber
    {
        protected IEventAggregator _eventAggregator;
        protected IFlashMessagesManager _flashMessagesManager;

        public BaseSubscriber(
            IEventAggregator eventAggregator,
            IFlashMessagesManager flashMessagesManager
        ) {
            _eventAggregator = eventAggregator;
            _flashMessagesManager = flashMessagesManager;

            eventAggregator.Subscribe(this);
        }


        protected Common.FlashMessages.Type GetMessageTypeByResultMessageSeverity(ResultObjectMessageSeverity s)
        {
            Common.FlashMessages.Type t;
            switch (s) {
                case ResultObjectMessageSeverity.SUCCESS:
                    t = Common.FlashMessages.Type.SUCCESS;
                    break;

                case ResultObjectMessageSeverity.WARNING:
                    t = Common.FlashMessages.Type.WARNING;
                    break;

                case ResultObjectMessageSeverity.ERROR:
                    t = Common.FlashMessages.Type.ERROR;
                    break;

                default:
                    t = Common.FlashMessages.Type.INFO;
                    break;
            }

            return t;
        }
    }
}
