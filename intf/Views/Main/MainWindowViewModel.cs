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
using Common.Overlay;

namespace intf.Views
{
    public class MainWindowViewModel :
        BaseConductorOneActive,
        IHandle<IChangeViewMessage<BaseViewModels.IViewModel>>
    {
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
                NewAccountViewModel vm = PrepareViewModel(() => { return new NewAccountViewModel(_accountFacade); });
                vm.IsCancelButtonVisible = false;
                vm.WindowTitle.Text = "Create your first Account";
                vm.IsDefaultAccount = true;
                IOverlayToken t = new OverlayToken(vm);
                vm.OnSuccessfullAccountCreation += (sender, account) => {
                    _accounts.Add(account);
                    Identity.Account = account;
                    NotifyOfPropertyChange(() => Identity);
                    t.HideOverlay();
                };

                Overlay.DisplayOverlay(t);

            } else {
                Identity.Account = (from Account p in _accounts where p.IsDefault = true select p).First();
                NotifyOfPropertyChange(() => Identity);
            }
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