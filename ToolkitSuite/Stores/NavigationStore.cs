using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Stores
{
    public static class NavigationStore
    {
        static public MainWindowViewModel mwvm;

        static private ViewModelBase _currentViewModel;
        static public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;

            set
            {
                _currentViewModel = value;
                OnCurrentVireModelChanged();
            }
        }

        public static LogicSettingsViewModel LogicSettingsViewModel;

        //Notify that the currentViewModel changed
        public static event Action CurrentViewModelChanged;

        private static void OnCurrentVireModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
