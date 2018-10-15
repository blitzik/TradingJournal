using Common.Commands;
using intf.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Views
{
    public class ConfirmationViewModel : BaseScreen
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        private DelegateCommand<object> _yesCommand;
        public DelegateCommand<object> YesCommand
        {
            get
            {
                if (_yesCommand == null) {
                    _yesCommand = new DelegateCommand<object>(p => ProcessYes());
                }
                return _yesCommand;
            }
        }


        private DelegateCommand<object> _cancelCommand;
        public DelegateCommand<object> CancelCommand
        {
            get
            {
                if (_cancelCommand == null) {
                    _cancelCommand = new DelegateCommand<object>(p => ProcessCancel());
                }
                return _cancelCommand;
            }
        }


        public ConfirmationViewModel()
        {
        }


        public event Action OnYesClicked;
        private void ProcessYes()
        {
            OnYesClicked?.Invoke();
        }


        public event Action OnCancelClicked;
        private void ProcessCancel()
        {
            OnCancelClicked?.Invoke();
        }

    }
}
