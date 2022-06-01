using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitSuite.Models;

namespace ToolkitSuite.Helpers
{
    public static class ItemUIConverter
    {
        public static Rule MotorVariablesToRule(List<LogicItemUI> items, Rule selectedRule)
        {
            Rule rule = selectedRule;
            List<MotorVarsOperator> motorVarsOperators = new List<MotorVarsOperator>();

            //Split items in vars and operators
            List<LogicItemUI> vars = new List<LogicItemUI>();
            List<LogicItemUI> operators = new List<LogicItemUI>();

            for (int i = 0; i < items.Count; i++)
            {
                if (i % 2 == 0)
                    vars.Add(items[i]);
                else
                    operators.Add(items[i]);
            }

            //Variables
            for (int i = 0; i < vars.Count; i++)
            {
                if (rule.MotorVariables.Count > i)
                {
                    //Exists
                    rule.MotorVariables[i].Name = vars[i].Value.ToString();
                }
                else
                {
                    //New
                    MotorVariable mv = new MotorVariable(vars[i].Value.ToString());
                    rule.MotorVariables.Add(mv);
                }
            }

            //Operators
            for (int i = 0; i < operators.Count; i++)
            {
                if (rule.MotorVarsOperators.Count > i)
                {
                    //Exists
                    Enum.TryParse(operators[i].SelectedValue.ToString(), out MotorVarsOperator varOperator);
                    rule.MotorVarsOperators[i] = varOperator;
                }
                else
                {
                    //New
                    Enum.TryParse(operators[i].SelectedValue.ToString(), out MotorVarsOperator varOperator);
                    rule.MotorVarsOperators.Add(varOperator);
                }
            }

            return rule;
        }

        /*
        public MotorVariable()
        {
            Name = "";
            RulesConditions = new List<RuleCondition>();
            ConditionsOperators = new List<ConditionsOperator>();
            Changes = false;
        }
        */

        public static RuleCondition RuleConditonValuesToRuleCondition(List<LogicItemUI> items, RuleCondition selectedRuleCondition)
        {
            RuleCondition ruleCondition = selectedRuleCondition;
            List<ValuesOperator> conditionsOperators = new List<ValuesOperator>();

            //Split items in vars and operators
            List<LogicItemUI> values = new List<LogicItemUI>();
            List<LogicItemUI> operators = new List<LogicItemUI>();

            for (int i = 0; i < items.Count; i++)
            {
                if (i % 2 == 0)
                    values.Add(items[i]);
                else
                    operators.Add(items[i]);
            }

            //Conditions
            for (int i = 0; i < values.Count; i++)
            {
                if (ruleCondition.Values.Count > i)
                {
                    //Exists
                    ruleCondition.Values[i] = values[i].Value.ToString();
                }
                else
                {
                    //New
                    ruleCondition.Values.Add(values[i].Value.ToString());
                }
            }

            //Operators
            for (int i = 0; i < operators.Count; i++)
            {
                if (ruleCondition.ValuesOperators.Count > i)
                {
                    //Exists
                    Enum.TryParse(operators[i].SelectedValue.ToString(), out ConditionsOperator valueOperator);
                    ruleCondition.ValuesOperators[i] = valueOperator;
                }
                else
                {
                    //New
                    Enum.TryParse(operators[i].SelectedValue.ToString(), out ConditionsOperator valueOperator);
                    ruleCondition.ValuesOperators.Add(valueOperator);
                }
            }

            return ruleCondition;
        }
    }
}
