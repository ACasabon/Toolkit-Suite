using System.Collections.Generic;
using ToolkitSuite.Models;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
    public class AddRuleEditorValueCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public AddRuleEditorValueCommand(LogicSettingsViewModel logicVM)
        {
            _lsvm = logicVM;
        }

        public override void Execute(object parameter)
        {

            if (_lsvm.ListFocus == 0)
            {
                RuleMotorVariablesAdded();
            }
            else
            {
                ConditionsAdded();
            }

            //_lsvm.NewRuleEditorValue();
            _lsvm.CheckForChanges();

            //Display Save Button as Active
            NavigationStore.mwvm.LogicUnsavedChanges = true;
        }

        void RuleMotorVariablesAdded()
        {
            int count = _lsvm.SelectedRule.MotorVariables.Count;
            int mvIndex = count - 1 < 0 ? 0 : count;
            List<RuleCondition> rcl = new List<RuleCondition>() { new RuleCondition(mvIndex, new List<object>() { "VALUE" }, new List<ConditionsOperator>(), new List<SettingsVariable>() { new SettingsVariable("VAR", "", "GROUP", "SUBGROUP") }) };
            MotorVariable mv = new MotorVariable("", rcl);
            _lsvm.SelectedRule.MotorVariables.Add(mv);
            _lsvm.SelectedRule.MotorVarsOperators.Add(0);
        }

        void ConditionsAdded()
        {
            _lsvm.SelectedRuleCondition.Values.Add("VALUE");
            _lsvm.SelectedRuleCondition.ValuesOperators.Add(0);
        }
    }
}
