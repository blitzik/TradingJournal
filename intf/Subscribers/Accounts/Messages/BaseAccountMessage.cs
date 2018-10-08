using prjt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Subscribers.Accounts.Messages
{
    public abstract class BaseAccountMessage
    {
        protected Account _account;
        public Account Account
        {
            get { return _account; }
        }


        public BaseAccountMessage(Account account)
        {
            _account = account;
        }
    }
}
