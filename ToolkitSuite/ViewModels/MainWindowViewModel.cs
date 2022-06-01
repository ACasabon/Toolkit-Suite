using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using ToolkitSuite.Commands;
using ToolkitSuite.Commands.Menu.PDFLogic;
using ToolkitSuite.Models;
using ToolkitSuite.Properties;
using ToolkitSuite.Stores;

namespace ToolkitSuite.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel => NavigationStore.CurrentViewModel;
        
        public Brush SaveIconColor => LogicUnsavedChanges ? new SolidColorBrush(Color.FromRgb(138, 0, 229)) : new SolidColorBrush(Colors.Gray);
        public Brush ActiveIconColor => _openLogicIsActive ? new SolidColorBrush(Color.FromRgb(255, 202, 24)) : new SolidColorBrush(Color.FromRgb(138, 0, 229));

        ProjectViewModel _projectVM;
        SuiteViewModel _suiteVM;
        OptionsViewModel _optionsVM;
        LogicSettingsViewModel _logicSettingsViewModel;
        int _selectedMenuTab;
        bool _displayPDFLogic = true;
        bool _logicUnsavedChanges;
        bool _openLogicIsActive => _logicSettingsViewModel.OpenLogicFilePath == Settings.Default.ActivePDFLogic;

        public int SelectedMenuTab
        {
            get { return _selectedMenuTab; }
            set
            {
                _selectedMenuTab = value;
                OnPropertyChanged(nameof(SelectedMenuTab));
                UpdateViewModel();
            }
        }

        public bool DisplayPDFLogic
        {
            get { return _displayPDFLogic; }
            set
            {
                _displayPDFLogic = value;
                OnPropertyChanged(nameof(DisplayPDFLogic));
            }
        }

        public bool LogicUnsavedChanges
        {
            get { return _logicUnsavedChanges; }
            set
            {
                _logicUnsavedChanges = value;
                OnPropertyChanged(nameof(LogicUnsavedChanges));
                OnPropertyChanged(nameof(SaveIconColor));
            }
        }

        public ICommand NewProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand SaveProjectCommand { get; }
        public ICommand ExportProjectCommand { get; }
        public ICommand GeneralSettingsCommand { get; }
        public ICommand InfoSettingsCommand { get; }

        //VIEW TAB
        public ICommand AddGroupViewCommand { get; }
        public ICommand AddSubgroupViewCommand { get; }
        public ICommand AddVariableViewCommand { get; }
        public ICommand RemoveVariableViewCommand { get; }
        public ICommand ImportViewCommand { get; }

        //LOGIC TAB
        public ICommand OpenPDFLogicTab { get; }
        public ICommand ClosePDFLogicTab { get; }
        public ICommand NewLogicCommand { get; }
        public ICommand SaveLogicCommand { get; }
        public ICommand ExportLogicCommand { get; }
        public ICommand ImportLogicCommand { get; }

        public MainWindowViewModel(ProjectViewModel projectVM, SuiteViewModel suiteVM, OptionsViewModel optionsVM, LogicSettingsViewModel logicSettingsViewModel, Func<InfoViewModel> CreateInfoViewModel)
        {
            NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _projectVM = projectVM;
            _suiteVM = suiteVM;
            _optionsVM = optionsVM;
            _logicSettingsViewModel = logicSettingsViewModel;
            GeneralSettingsCommand = new NavigateCommand(optionsVM);
            OpenPDFLogicTab = new OpenLogicTabCommand(this);
            ClosePDFLogicTab = new CloseLogicTabCommand(this);
            InfoSettingsCommand = new NavigateCommand(CreateInfoViewModel);
            SaveLogicCommand = new SaveLogicCommand(_logicSettingsViewModel);
            ImportLogicCommand = new ImportLogicCommand(_logicSettingsViewModel);
            ExportLogicCommand = new ExportLogicCommand(_logicSettingsViewModel);

            NavigationStore.LogicSettingsViewModel = _logicSettingsViewModel;
            NavigationStore.mwvm = this;

            //FOR testing
            OpenActiveLogic();
        }

        public void UpdateViewModel()
        {
            switch (SelectedMenuTab)
            {
                case 0:
                    NavigationStore.CurrentViewModel = _suiteVM;
                    break;
                case 1:
                    NavigationStore.CurrentViewModel = _projectVM;
                    break;
                case 2:
                    NavigationStore.CurrentViewModel = _optionsVM;
                    break;
                case 3:
                    NavigationStore.CurrentViewModel = _logicSettingsViewModel;
                    break;
            }
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void OpenActiveLogic()
        {
            string path = Settings.Default.ActivePDFLogic;
            if (string.IsNullOrEmpty(path))
                return;

            string json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json))
                return;

            _logicSettingsViewModel.Rules = JsonConvert.DeserializeObject<ObservableCollection<Rule>>(json);
            _logicSettingsViewModel.OpenLogicFilePath = path;


            OnPropertyChanged(nameof(ActiveIconColor));
        }
    }
}
