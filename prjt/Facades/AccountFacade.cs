using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Perst;
using prjt.Domain;
using prjt.Services;
using prjt.Services.Identity;

namespace prjt.Facades
{
    public class AccountFacade : BaseFacade
    {
        public AccountFacade(IIdentity identity, PerstStorageFactory perstStorageFactory, StoragePool storagePool) : base(identity, perstStorageFactory, storagePool)
        {
            _perstStorageFactory = perstStorageFactory;
        }


        public ResultObject<Account> StoreAccount(Account account)
        {
            Storage storage = Storage(DatabaseNames.ACCOUNTS);

            ResultObject<Account> result = new ResultObject<Account>(false);
            if (!Root<AccountsRoot>(DatabaseNames.ACCOUNTS).Accounts.Put(account)) {
                result.AddMessage("Account with given name already exists.");
                return result;
            }

            storage.Store(account);
            string accountPath = Path.Combine(PerstStorageFactory.GetDatabaseDirectoryPath(), account.Name);
            Directory.CreateDirectory(accountPath);

            Storage accDb = LoadAccount(account.Name);
            Root accRoot = (Root)accDb.Root;
            if (accRoot == null) {
                accRoot = new Root(accDb);
                accDb.Root = accRoot;
            }
            accRoot.StartingBalance = account.StartingBalance;
            accRoot.Balance = account.StartingBalance;

            storage.Commit();
            accDb.Commit();

            return new ResultObject<Account>(true, account);
        }


        public Storage LoadAccount(string accountName)
        {
            Storage db = _perstStorageFactory.OpenConnection<Root>(GetAccountDataStorageFilePath(accountName));
            StoragePool.Add(accountName, db); // todo user account cannot be named "accounts"

            return db;
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
