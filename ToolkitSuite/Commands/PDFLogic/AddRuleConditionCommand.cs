using System.Collections.Generic;
using System.ComponentModel;
using ToolkitSuite.Models;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
    public class AddRuleConditionCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public AddRuleConditionCommand(LogicSettingsViewModel lsvm)
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
            if (_lsvm.SelectedRuleCondition == null)
                return;
            int motorVarIndex = _lsvm.SelectedRuleCondition.MotorVariableIndex;
            List<RuleCondition> rcl = _lsvm.SelectedRule.MotorVariables[motorVarIndex].RulesConditions;
            RuleCondition ruleCondition = new RuleCondition(motorVarIndex, new List<object>() { "VALUE" }, new List<ConditionsOperator>(), new List<SettingsVariable>() { new SettingsVariable("VAR", "", "GROUP", "SUBGROUP") });
            rcl.Add(ruleCondition);
            _lsvm.SelectedRuleCondition = ruleCondition;
            _lsvm.ListFocus = 1;
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
