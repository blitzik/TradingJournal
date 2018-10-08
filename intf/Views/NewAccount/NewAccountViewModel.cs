using Caliburn.Micro;
using Common.Commands;
using Common.Overlay;
using Common.Validation;
using intf.BaseViewModels;
using intf.Subscribers.Accounts.Messages;
using intf.Utils;
using prjt.Domain;
using prjt.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intf.Views
{
    public class NewAccountViewModel : BaseScreen
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
                CreateAccountCommand.RaiseCanExecuteChanged();
            }
        }


        private int? _balance;
        public string Balance
        {
            get { return _balance.ToString(); }
            set
            {
                Set(ref _balance, IntegersOnlyUtils.ConvertOnlyPositive(value, _balance));
                CreateAccountCommand.RaiseCanExecuteChanged();
            }
        }


        private bool _isCancelButtonVisible;
        public bool IsCancelButtonVisible
        {
            get { return _isCancelButtonVisible; }
            set { Set(ref _isCancelButtonVisible, value); }
        }


        private bool _isDefaultAccount;
        public bool IsDefaultAccount
        {
            get { return _isDefaultAccount; }
            set { Set(ref _isDefaultAccount, value); }
        }


        private DelegateCommand<object> _createAccountCommand;
        public DelegateCommand<object> CreateAccountCommand
        {
            get
            {
                if (_createAccountCommand == null) {
                    _createAccountCommand = new DelegateCommand<object>(
                        p => CreateAccount(),
                        p => Validation.Check(nameof(Name), Name) && Validation.Check(nameof(Balance), ToNullableInt(Balance))
                    );
                }
                return _createAccountCommand;
            }
        }


        private AccountFacade _accountFacade;

        public NewAccountViewModel(AccountFacade accountFacade)
        {
            _accountFacade = accountFacade;

            IsCancelButtonVisible = true;
            IsDefaultAccount = false;
            Name = string.Empty;
            Balance = "100";

            BaseWindowTitle = "New Account";
        }


        protected override void InitializeValidation()
        {
            base.InitializeValidation();

            Validation.AddRuleSet(
                nameof(Name),
                new RuleSet<string>().AddRule(x => { return x.Length < 3; }, "Account name must have at least 3 characters")
            );

            Validation.AddRuleSet(
                nameof(Balance),
                new RuleSet<int?>().AddRule(x => { return x < 100; }, "Account balance must be positive integer number bigger than 100")
            );
        }


        // -----

        public event Action<object, Account> OnSuccessfullAccountCreation;
        private void CreateAccount()
        {
            Account newAccount = new Account(Name, (double)int.Parse(Balance), IsDefaultAccount);
            _accountFacade.StoreAccount(newAccount);

            EventAggregator.PublishOnUIThread(new AccountSuccessfullyCreatedMessage(newAccount));

            Action<object, Account> handler = OnSuccessfullAccountCreation;
            if (handler != null) {
                handler(this, newAccount);
            }
        }


        private int? ToNullableInt(string s)
        {
            if (int.TryParse(s, out int i)) return i;
            return null;
        }
    }
}
