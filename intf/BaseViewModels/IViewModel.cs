using prjt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.BaseViewModels
{
    public interface IViewModel : Common.ViewModels.IViewModel
    {
        string BaseWindowTitle { get; set; }
        PageTitle WindowTitle { get; set; }

        ISecondNavigationViewModel SecondNavigation { get; set; }
        bool IsSecondNavigationActive { get; set; }
    }
}
