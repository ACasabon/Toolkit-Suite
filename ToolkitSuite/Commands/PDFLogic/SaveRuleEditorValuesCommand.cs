using System.Collections.Generic;
using System.Linq;
using ToolkitSuite.Helpers;
using ToolkitSuite.Models;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
	public class SaveRuleEditorValuesCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public SaveRuleEditorValuesCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
        }

        public override void Execute(object parameter)
        {
            if (_lsvm.ListFocus == 0)
            {
                RuleMotorVariablesChanged();
            }
            else
            {
                ConditionsChanged();
            }

            _lsvm.CheckForChanges();
        }

        void RuleMotorVariablesChanged()
        {
            int index = _lsvm.SelectedRuleIndex;
            Rule rule = ItemUIConverter.MotorVariablesToRule(_lsvm.SelectedItemRules.ToList(), _lsvm.SelectedRule);
            _lsvm.Rules.RemoveAt(index);
            if (_lsvm.Rules.Count > 0)
                _lsvm.Rules.Insert(index, rule);
            else
                _lsvm.Rules.Add(rule);
            _lsvm.SelectedRule = rule;
        }

        void ConditionsChanged()
        {
            int index = _lsvm.SelectedRuleConditionIndex;
            RuleCondition ruleCondition = ItemUIConverter.RuleConditonValuesToRuleCondition(_lsvm.SelectedItemRules.ToList(), _lsvm.SelectedRuleCondition);
            List<RuleCondition> rulesConditions = _lsvm.Rules[_lsvm.SelectedRuleIndex].MotorVariables[ruleCondition.MotorVariableIndex].RulesConditions;
            rulesConditions.RemoveAt(index);
            if (rulesConditions.Count > 0)
                rulesConditions.Insert(index, ruleCondition);
            else
                rulesConditions.Add(ruleCondition);
            _lsvm.SelectedRuleCondition = ruleCondition;
        }
    }
}
