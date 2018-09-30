using Caliburn.Micro;
using Common.ViewModels;
using System;

namespace Common.ViewModelResolver
{
    public class ViewModelResolver : IViewModelResolver
    {
        private readonly SimpleContainer _container;


        public ViewModelResolver(SimpleContainer container)
        {
            _container = container;
        }


        public VM Resolve<VM>(System.Type viewModel) where VM : IViewModel
        {
            VM vm = (VM)_container.GetInstance(viewModel, null);
            if (vm != null) {
                _container.BuildUp(vm);
            }

            return vm;
        }


        public VM Resolve<VM>() where VM : IViewModel
        {
            VM vm = Resolve<VM>(typeof(VM));
            if (vm != null) {
                _container.BuildUp(vm);
            }

            return vm;
        }


        public VM BuildUp<VM>(VM instance) where VM : IViewModel
        {
            _container.BuildUp(instance);

            return instance;
        }
    }
}
