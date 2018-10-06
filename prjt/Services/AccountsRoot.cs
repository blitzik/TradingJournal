using Perst;
using prjt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Services
{
    public class AccountsRoot : Persistent
    {
        private FieldIndex<string, Account> _accounts;
        public FieldIndex<string, Account> Accounts
        {
            get { return _accounts; }
        }


        public AccountsRoot(Storage db)
        {
            _accounts = db.CreateFieldIndex<string, Account>("_name", true);
        }
    }
}
