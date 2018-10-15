using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Services
{
    public static class DatabaseNames
    {
        public const string ACCOUNTS = "accounts";


        public static string GetAccountDataStorageFilePath(string accountName)
        {
            string path = Path.Combine(PerstStorageFactory.GetDatabaseDirectoryPath(), accountName);

            return Path.Combine(path, string.Format("{0}_data.{1}", accountName, PerstStorageFactory.DATABASE_EXTENSION));
        }
    }
}
