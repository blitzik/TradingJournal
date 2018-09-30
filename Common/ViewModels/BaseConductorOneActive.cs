using Caliburn.Micro;
using Common.FlashMessages;
using Common.ViewModelResolver;
using Common.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.ViewModels;
using Common.Overlay;

namespace Common.ViewModels
{
    public abstract class BaseConductorOneActive<P> : Conductor<P>.Collection.OneActive, IViewModel, INotifyDataErrorInfo where P : class, IViewModel
    {
        // property injection
        private IEventAggregator _eventAggregator;
        public IEventAggregator EventAggregator
        {
            get { return _eventAggregator; }
            set { _eventAggregator = value; }
        }


        // property injection
        private IViewModelResolver _viewModelResolver;
        public IViewModelResolver ViewModelResolver
        {
            get { return _viewModelResolver; }
            set { _viewModelResolver = value; }
        }


        // property injection
        private IFlashMessagesManager _flashMessagesManager;
        public IFlashMessagesManager FlashMessagesManager
        {
            get { return _flashMessagesManager; }
            set { _flashMessagesManager = value; }
        }


        // property injection
        private IOverlay _overlay;
        public IOverlay Overlay
        {
            get { return _overlay; }
            set { _overlay = value; }
        }


        public virtual void ActivateItem<VM>() where VM : P
        {
            ActivateItem(GetViewModel<VM>());
        }


        protected P GetViewModel<VM>() where VM : P
        {
            VM vm = _viewModelResolver.Resolve<VM>();
            if (vm == null) {
                throw new Exception("Requested ViewModel does not Exist!");
            }

            return vm;
        }


        protected P GetViewModel(System.Type viewModel)
        {
            return _viewModelResolver.Resolve<P>(viewModel);
        }


        protected VM PrepareViewModel<VM>() where VM : IViewModel, new()
        {
            VM vm = Activator.CreateInstance<VM>();
            ViewModelResolver.BuildUp(vm);

            return vm;
        }


        protected VM PrepareViewModel<VM>(Func<VM> instantiation) where VM : IViewModel
        {
            VM vm = instantiation.Invoke();
            ViewModelResolver.BuildUp(vm);

            return vm;
        }


        protected override void OnInitialize()
        {
            base.OnInitialize();

            InitializeValidation();
        }


        // ----- INotifyPropertyChanged


        public override bool Set<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (Validation != null) {
                Validation.Check(propertyName, newValue, true);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            return base.Set(ref oldValue, newValue, propertyName);
        }


        // ----- INotifyDataErrorInfo


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        // property injection
        private IValidationObject _validation;
        public IValidationObject Validation
        {
            get { return _validation; }
            set
            {
                _validation = value;
                _validation.ErrorsChanged += (object sender, DataErrorsChangedEventArgs e) => {
                    ErrorsChanged?.Invoke(this, e);
                };
            }
        }


        protected virtual void InitializeValidation()
        {
        }


        public bool HasErrors
        {
            get { return Validation.HasErrors; }
        }


        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) {
                // todo
                return new List<string>();
            }

            if (!Validation.Errors.ContainsKey(propertyName)) {
                return new List<string>();
            }

            return Validation.Errors[propertyName];
        }

    }
}
