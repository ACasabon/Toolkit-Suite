using System.ComponentModel;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
	public class RemoveRuleEditorValueCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public override bool CanExecute(object parameter)
        {
            bool selected = _lsvm.SelectedItemRule != null;
            bool index = _lsvm.SelectedItemIndex != 0;
            bool notOperator = _lsvm.SelectedItemRule?.SelectedValue == null;
            return selected && index && notOperator && base.CanExecute(parameter);
        }

        public RemoveRuleEditorValueCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
            _lsvm.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override void Execute(object parameter)
        {
            if (_lsvm.ListFocus == 0)
            {
                RuleMotorVariablesRemoved();
            }
            else
            {
                ConditionsRemoved();
            }

            _lsvm.CheckForChanges();

            //Display Save Button as Active
            NavigationStore.mwvm.LogicUnsavedChanges = true;
        }

        void RuleMotorVariablesRemoved()
        {
            int index = _lsvm.SelectedItemIndex - 1;
            _lsvm.SelectedRule.MotorVariables.RemoveAt(index);
            _lsvm.SelectedRule.MotorVarsOperators.RemoveAt(index - 1);
        }

        void ConditionsRemoved()
        {
            int index = _lsvm.SelectedItemIndex - 1;
            _lsvm.SelectedRuleCondition.Values.RemoveAt(index);
            _lsvm.SelectedRuleCondition.ValuesOperators.RemoveAt(index - 1);
        }

        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_lsvm.SelectedItemRule))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
