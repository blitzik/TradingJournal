using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjt.Services.IO
{
    public interface IIODialogService
    {
        string GetFilePath<T>(string defaultFilePath, Action<T> modifier) where T : FileDialog;
    }
}
