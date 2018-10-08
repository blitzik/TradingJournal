using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjt.Domain;

namespace intf.Subscribers.Accounts.Messages
{
    public class AccountSuccessfullyCreatedMessage : BaseAccountMessage
    {
        public AccountSuccessfullyCreatedMessage(Account account) : base(account)
        {
        }
    }
}
