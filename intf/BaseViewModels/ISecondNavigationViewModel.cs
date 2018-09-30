using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.BaseViewModels
{
    public interface ISecondNavigationViewModel
    {
        IViewModel CurrentlyActivatedItem { get; set; }
    }
}
