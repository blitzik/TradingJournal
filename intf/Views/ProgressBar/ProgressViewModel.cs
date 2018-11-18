using Common.Commands;
using intf.BaseViewModels;

namespace intf.Views
{
    public class ProgressViewModel : BaseScreen
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }


        public ProgressViewModel()
        {
            Text = "Probíhá zpracování Vašeho požadavku";
        }
    }
}
