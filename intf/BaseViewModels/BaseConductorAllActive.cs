using prjt.Domain;
using prjt.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.BaseViewModels
{
    public abstract class BaseConductorAllActive : Common.ViewModels.BaseConductorAllActive<IViewModel>, IViewModel
    {
        protected PageTitle _windowTitle = new PageTitle();
        public PageTitle WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                Set(ref _windowTitle, value);
            }
        }


        protected string _baseWindowTitle;
        public string BaseWindowTitle
        {
            get { return _baseWindowTitle; }
            set
            {
                _baseWindowTitle = value;
                WindowTitle.Text = value;
            }
        }


        // property injection
        private IIdentity _identity;
        public IIdentity Identity
        {
            get { return _identity; }
            set { Set(ref _identity, value); }
        }


        protected ISecondNavigationViewModel _secondNavigation;
        public ISecondNavigationViewModel SecondNavigation
        {
            get { return _secondNavigation; }
            set { Set(ref _secondNavigation, value); }
        }


        protected bool _isSecondNavigationActive;
        public bool IsSecondNavigationActive
        {
            get { return _isSecondNavigationActive; }
            set { Set(ref _isSecondNavigationActive, value); }
        }
    }
}
