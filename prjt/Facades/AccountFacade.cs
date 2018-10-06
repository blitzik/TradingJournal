using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perst;
using prjt.Domain;
using prjt.Services;

namespace prjt.Facades
{
    public class AccountFacade : BaseFacade
    {
        private PerstStorageFactory _perstStorageFactory;


        public AccountFacade(PerstStorageFactory perstStorageFactory, StoragePool storagePool) : base(storagePool)
        {
            _perstStorageFactory = perstStorageFactory;
        }


        public Account StoreAccount(Account account)
        {
            Storage(DatabaseNames.ACCOUNTS).Store(account);
            Root<AccountsRoot>(DatabaseNames.ACCOUNTS).Accounts.Put(account);

            // todo create database for account data

            return account;
        }


        public void LoadAccount(string name)
        {
            Storage db = _perstStorageFactory.OpenConnection<Root>(name, 16 * 1024 * 1024);
            StoragePool.Add(name, db);
        }


        public void UnloadAccount(string name)
        {

        }


        public IList<Account> FindAccounts()
        {
            return new List<Account>(Root<AccountsRoot>(DatabaseNames.ACCOUNTS).Accounts);
        }
    }
}
