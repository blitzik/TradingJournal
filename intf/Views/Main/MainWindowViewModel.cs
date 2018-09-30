using Caliburn.Micro;
using prjt.Domain;
using intf.Messages;
using System.Reflection;
using Common.EventAggregator.Messages;
using intf.BaseViewModels;
using Common.Commands;
using System.Threading.Tasks;

namespace intf.Views
{
    public class MainWindowViewModel :
        BaseConductorOneActive,
        IHandle<IChangeViewMessage<BaseViewModels.IViewModel>>
    {
        private PageTitle _title = new PageTitle();
        public PageTitle Title
        {
            get { return _title; }
            private set
            {
                Set(ref _title, value);
            }
        }


        private string _version;
        public string AppVersion
        {
            get { return _version; }
        }


        private MainNavigationViewModel _mainNavigationViewModel;
        public MainNavigationViewModel MainNavigationViewModel
        {
            get { return _mainNavigationViewModel; }
        }


        public MainWindowViewModel()
        {
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }


        // -----


        protected override void OnInitialize()
        {
            base.OnInitialize();

            EventAggregator.Subscribe(this);

            _mainNavigationViewModel = PrepareViewModel(() => { return new MainNavigationViewModel(); });
            _mainNavigationViewModel.ConductWith(this);
            NotifyOfPropertyChange(() => MainNavigationViewModel);
        }       


        public override void ActivateItem(BaseViewModels.IViewModel item)
        {
            Title = item.WindowTitle;

            base.ActivateItem(item);

            _mainNavigationViewModel.ActiveItem = item;
        }


        // -----


        public void Handle(IChangeViewMessage<BaseViewModels.IViewModel> message)
        {
            IViewModel vm;
            if (message.ViewModel != null) {
                vm = message.ViewModel;
            } else {
                vm = GetViewModel(message.Type);
            }

            if (vm == ActiveItem) {
                return;
            }
            message.Apply(vm);
            ActivateItem(vm);
        }
    }
}