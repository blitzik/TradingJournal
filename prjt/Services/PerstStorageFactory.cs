using Perst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Services
{
    public class PerstStorageFactory
    {
        public const string DATABASE_EXTENSION = "evdo";
        public const string MAIN_DATABASE_NAME = "data";
        public const string MAIN_DATABASE_FILE_NAME = MAIN_DATABASE_NAME + "." + DATABASE_EXTENSION;


        public PerstStorageFactory()
        {
        }


        public Storage OpenConnection(string databaseName)
        {
            Storage db = StorageFactory.Instance.CreateStorage();
            db.Open(Path.Combine(GetDatabaseDirectoryPath(), string.Format("{0}.{1}", databaseName, DATABASE_EXTENSION)), 4 * 1024 * 1024);

            Root root = db.Root as Root;
            if (root == null) {
                root = new Root(db);
                db.Root = root;
            }

            return db;
        }


        public static string GetDatabaseDirectoryPath()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "_evidooApp");
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
