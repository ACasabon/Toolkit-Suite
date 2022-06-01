using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToolkitSuite.Views;
using ToolkitSuite.ViewModels;
using ToolkitSuite.Stores;
using ToolkitSuite.Properties;

namespace ToolkitSuite
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowViewModel _mainViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            SuiteViewModel suiteViewModel = new SuiteViewModel();
            ProjectViewModel projectViewModel = new ProjectViewModel();
            OptionsViewModel optionsViewModel = new OptionsViewModel();
            LogicSettingsViewModel logicSettingsViewModel = new LogicSettingsViewModel();
            _mainViewModel = new MainWindowViewModel(projectViewModel, suiteViewModel, optionsViewModel, logicSettingsViewModel, CreateInfoViewModel);

            NavigationStore.CurrentViewModel = suiteViewModel;

            SetupSettings();

            MainWindow = new MainWindow()
            {
                DataContext = _mainViewModel
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        private InfoViewModel CreateInfoViewModel()
        {
            return new InfoViewModel();
        }

        private void SetupSettings()
        {
            //Only Executed the first time the app is launched
            if (!Settings.Default.InitializedPaths)
            {
                Settings.Default.PDFLogicFilesFolder = AppDomain.CurrentDomain.BaseDirectory + "Logic";
                Settings.Default.ActivePDFLogic = AppDomain.CurrentDomain.BaseDirectory + @"Logic\Base.json";
                Settings.Default.InitializedPaths = true;

                Settings.Default.Save();
            }
        }
    }
}
