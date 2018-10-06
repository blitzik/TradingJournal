using prjt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Services.Identity
{
    public class Identity : IIdentity
    {
        private Account _account;
        public Account Account
        {
            get { return _account; }
            set { _account = value; }
        }
    }
}
