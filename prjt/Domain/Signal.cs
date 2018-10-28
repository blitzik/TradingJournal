using Caliburn.Micro;
using Perst;
using prjt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjt.Domain
{
    public class Signal : PropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }


        public Signal() { }

        public Signal(string name)
        {
            Name = name;
        }

        
    }
}
