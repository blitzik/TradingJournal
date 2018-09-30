using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjt.Services.IO
{
    public class FilePathDialogService : IIODialogService
    {
        public string GetFilePath<T>(string defaultFilePath, Action<T> modifier) where T : FileDialog
        {
            T d = (T)Activator.CreateInstance(typeof(T));
            if (!string.IsNullOrEmpty(defaultFilePath)) {
                d.FileName = defaultFilePath;
            }
            modifier.Invoke(d);

            if (d.ShowDialog() == DialogResult.OK) {
                return d.FileName;
            }

            return null;
        }
    }
}
