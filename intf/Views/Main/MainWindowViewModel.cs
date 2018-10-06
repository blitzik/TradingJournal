using Caliburn.Micro;
using prjt.Domain;
using intf.Messages;
using System.Reflection;
using Common.EventAggregator.Messages;
using intf.BaseViewModels;
using Common.Commands;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using prjt.Facades;
using prjt.Services;

namespace intf.Views
{
    public class MainWindowViewModel :
        BaseConductorOneActive,
        IHandle<IChangeViewMessage<BaseViewModels.IViewModel>>
    {
        private Account _selectedProfile;
        public Account SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                Set(ref _selectedProfile, value);
            }
        }


        private ObservableCollection<Account> _accounts;
        private ReadOnlyObservableCollection<Account> _accountsReadOnly;
        public ReadOnlyObservableCollection<Account> Accounts
        {
            get { return _accountsReadOnly; }
        }


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


        private IViewModel _overlayViewModel;
        public IViewModel OverlayViewModel
        {
            get { return _overlayViewModel; }
            private set
            {
                Set(ref _overlayViewModel, value);
            }
        }


        private readonly AccountFacade _accountFacade;

        public MainWindowViewModel(AccountFacade accountFacade)
        {
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            _accountFacade = accountFacade;
        }


        // -----


        protected override void OnInitialize()
        {
            base.OnInitialize();

            EventAggregator.Subscribe(this);

            _mainNavigationViewModel = PrepareViewModel(() => { return new MainNavigationViewModel(); });
            _mainNavigationViewModel.ConductWith(this);
            NotifyOfPropertyChange(() => MainNavigationViewModel);

            IList<Account> accounts = _accountFacade.FindAccounts();
            _accounts = new ObservableCollection<Account>(accounts);
            NotifyOfPropertyChange(() => Accounts);

            _accountsReadOnly = new ReadOnlyObservableCollection<Account>(_accounts);
            if (accounts.Count < 1) {
                NewAccountViewModel vm = PrepareViewModel(() => { return new NewAccountViewModel(); });
                vm.IsCancelButtonVisible = false;
                vm.WindowTitle.Text = "Create your first Account";
                vm.OnCreatedAccount += (sender, account) => {
                    _accounts.Add(account);
                };

                ScreenExtensions.TryActivate(vm);
                OverlayViewModel = vm;
                Overlay.DisplayOverlay(vm);
            }

            // todo first must complete new account creation and then we can select an account from _accounts
            //SelectedProfile = (from Account p in _accounts where p.IsDefault = true select p).First();
        }


        private void LoadAccount(Account account)
        {

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