using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using Common.Validation;
using Common.FlashMessages;
using Common.ViewModelResolver;
using intf.Views;
using System.Threading;
using prjt.Services;
using prjt.Services.IO;
using prjt.Facades;
using prjt.Domain;
using Perst;
using intf.BaseViewModels;
using intf.Subscribers;
using Common.Utils.ResultObject;
using Common.Overlay;
using prjt.Services.Identity;

namespace TradingJournal
{
    public class Bootstrapper : BootstrapperBase
    {
        static Mutex mutex = new Mutex(false, "34515d3d-cdda-4d87-aa0c-eeaab04ba20a");


        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }


        protected override void Configure()
        {
            var config = new TypeMappingConfiguration()
            {
                DefaultSubNamespaceForViews = "intf.Views",
                DefaultSubNamespaceForViewModels = "intf.Views"
            };
            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);


            // -----


            _container = new SimpleContainer();
            _container.Instance(_container);

            // Common
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.Singleton<IViewModelResolver, ViewModelResolver>();
            _container.Singleton<IFlashMessagesManager, FlashMessagesManager>();
            _container.PerRequest<IValidationObject, ValidationObject>();
            _container.Singleton<IOverlay, Overlay>();

            // Services
            _container.Singleton<IIdentity, Identity>();
            _container.Singleton<PerstStorageFactory>();
            _container.Singleton<StoragePool>();
            _container.Singleton<IIODialogService, FilePathDialogService>();

            // facades
            _container.Singleton<AccountFacade>();

            // Windows
            _container.Singleton<MainWindowViewModel>();

            // ViewModels
            _container.PerRequest<NewAccountViewModel>();

            // Subscribers
        }


        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            /*if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false)) {
                System.Windows.Application.Current.Shutdown();
            }*/

            ResultObject<object> ro = new ResultObject<object>(true);
            try {
                Storage profilesStorage = _container.GetInstance<PerstStorageFactory>().OpenConnection<AccountsRoot>(DatabaseNames.ACCOUNTS);
                StoragePool sp = _container.GetInstance<StoragePool>();
                sp.Add(DatabaseNames.ACCOUNTS, profilesStorage);

                AccountsRoot profilesRoot = (AccountsRoot)profilesStorage.Root;

                var vm = _container.GetInstance<MainWindowViewModel>();
                _container.BuildUp(vm);
                _container.GetInstance<IWindowManager>().ShowWindow(vm);

            } catch (StorageError ex) {
                ro = new ResultObject<object>(false);
                ro.AddMessage("Nelze načíst Vaše data.");

            } catch (Exception ex) {
                ro = new ResultObject<object>(false);
                ro.AddMessage("Při spouštění aplikace došlo k neočekávané chybě");
            }

            if (!ro.Success) {// todo
                /*StartupErrorWindowViewModel errw = _container.GetInstance<StartupErrorWindowViewModel>();
                errw.Text = ro.GetLastMessage().Text;
                _container.GetInstance<IWindowManager>().ShowDialog(errw);*/
            }
        }


        protected override void OnExit(object sender, EventArgs e)
        {
            _container.GetInstance<StoragePool>().CloseAll();
            //mutex.ReleaseMutex();
        }


        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {
                GetType().Assembly,
                typeof(MainWindowView).Assembly
            };
        }


        protected override IEnumerable<object> GetAllInstances(System.Type service)
        {
            return _container.GetAllInstances(service);
        }


        protected override object GetInstance(System.Type service, string key)
        {
            return _container.GetInstance(service, key);
        }


        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
