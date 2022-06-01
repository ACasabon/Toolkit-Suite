using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly Func<ViewModelBase> _createViewModel;
        private readonly ViewModelBase _staticViewModel;

        public NavigateCommand(Func<ViewModelBase> CreateViewModel)
        {
            _createViewModel = CreateViewModel;
        }

        public NavigateCommand(ViewModelBase StaticViewModel)
        {
            _staticViewModel = StaticViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_staticViewModel == null)
                NavigationStore.CurrentViewModel = _createViewModel();
            else
                NavigationStore.CurrentViewModel = _staticViewModel;
        }
    }
}
