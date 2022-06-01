using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using ToolkitSuite.Models;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
	public class RemoveRuleConditionCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public RemoveRuleConditionCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
            _lsvm.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            bool can = _lsvm.SelectedRule != null && _lsvm.SelectedRuleCondition != null;
            bool count = false;
            if(can)
            {
                int motorVarIndex = _lsvm.SelectedRuleCondition.MotorVariableIndex;
                count = _lsvm.SelectedRule.MotorVariables[motorVarIndex].RulesConditions.Count > 1;
            }
            return can && count && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            //MultipleErase(parameter);

            //Erase selected rule
            int motorVarIndex = _lsvm.SelectedRuleCondition.MotorVariableIndex;
            List<RuleCondition> rcl = _lsvm.SelectedRule.MotorVariables[motorVarIndex].RulesConditions;
            rcl.Remove(_lsvm.SelectedRuleCondition);

            _lsvm.SelectedRuleCondition = null;

            _lsvm.CheckForChanges();

            //Display Save Button as Active
            NavigationStore.mwvm.LogicUnsavedChanges = true;
        }

        void MultipleErase(object parameter)
        {
            //Get selected Rules
            List<RuleCondition> ruleConditions = new List<RuleCondition>();
            if (parameter is IEnumerable)
            {
                var enumerator = ((IEnumerable)parameter).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ruleConditions.Add((RuleCondition)enumerator.Current);
                }
            }

            //Erase selected rules
            int motorVarIndex = _lsvm.SelectedRuleCondition.MotorVariableIndex;
            List<RuleCondition> rcl = _lsvm.SelectedRule.MotorVariables[motorVarIndex].RulesConditions;
            foreach (var ruleCondition in ruleConditions)
            {
                rcl.Remove(ruleCondition);
            }
        }

        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {            
            if (e.PropertyName == nameof(_lsvm.SelectedRuleCondition) || e.PropertyName == nameof(_lsvm.SelectedRule))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
