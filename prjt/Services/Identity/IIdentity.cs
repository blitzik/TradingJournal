using prjt.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Services.Identity
{
    public interface IIdentity
    {
        Account Account { get; set; }
    }
}
