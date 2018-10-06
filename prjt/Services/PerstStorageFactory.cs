﻿using Perst;
using prjt.Domain;
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
        public const string DATABASE_EXTENSION = "tjd";


        public PerstStorageFactory()
        {
        }


        public Storage OpenConnection<TRoot>(string databaseName, int pageSize = 4 * 1024 * 1024)
        {
            Storage db = StorageFactory.Instance.CreateStorage();
            db.Open(Path.Combine(GetDatabaseDirectoryPath(), string.Format("{0}.{1}", databaseName, DATABASE_EXTENSION)), pageSize);

            TRoot root;
            if (db.Root == null) {
                root = (TRoot)Activator.CreateInstance(typeof(TRoot), db);
                db.Root = root;
            }

            return db;
        }


        public static string GetDatabaseDirectoryPath()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "_tradingJournal");
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
