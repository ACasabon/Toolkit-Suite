using System.Collections.Generic;
using ToolkitSuite.Models;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
    public class AddRuleCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public AddRuleCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
        }

        public override void Execute(object parameter)
        {
            List<RuleCondition> rcl = new List<RuleCondition>() { new RuleCondition(0, new List<object>() { "VALUE" }, new List<ConditionsOperator>(), new List<SettingsVariable>() { new SettingsVariable("VAR", "", "GROUP", "SUBGROUP") }) };
            MotorVariable mv = new MotorVariable("NEW RULE", rcl);
            List<MotorVariable> mvl = new List<MotorVariable>() { mv };
            Rule rule = new Rule(mvl);
            _lsvm.Rules.Add(rule);
            _lsvm.SelectedRule = rule;
            _lsvm.ListFocus = 0;
            _lsvm.UpdateRuleEditor();

            //Display Save Button as Active
            NavigationStore.mwvm.LogicUnsavedChanges = true;
        }
    }
}
