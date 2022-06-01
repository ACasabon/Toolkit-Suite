using System.ComponentModel;
using ToolkitSuite.Models;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
    public class AddSettingsVariableCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public AddSettingsVariableCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
            _lsvm.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            bool can = _lsvm.SelectedRuleCondition != null;
            return can && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            SettingsVariable sv = new SettingsVariable("VAR", "", "GROUP", "SUBGROUP");
            _lsvm.SelectedRuleCondition.Variables.Add(sv);
            _lsvm.CheckForChanges();

            //Display Save Button as Active
            NavigationStore.mwvm.LogicUnsavedChanges = true;
        }

        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_lsvm.SelectedRuleCondition))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
