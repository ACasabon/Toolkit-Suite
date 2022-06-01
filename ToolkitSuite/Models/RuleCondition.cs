using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolkitSuite.Models
{
    public class RuleCondition
    {
        public RuleCondition(int motorVariableIndex, List<object> values, List<ConditionsOperator> valuesOperators, List<SettingsVariable> variables)
        {
            MotorVariableIndex = motorVariableIndex;
            Values = values;
            ValuesOperators = valuesOperators;
            Variables = variables;
        }

        public string Name => GetRuleConditionName();

        public int MotorVariableIndex { get; set; }
        public List<object> Values { get; set; }
        public List<ConditionsOperator> ValuesOperators { get; set; }
        public List<SettingsVariable> Variables { get; set; }
        public bool Changes { get; set; }

        string GetRuleConditionName()
        {
            string name = null;

            if (Values.Count == 1)
            {
                name = Values[0].ToString();
            }
            else if (Values.Count > 0)
            {
                for (int i = 0; i < Values.Count - 1; i++)
                {
                    name += Values[i].ToString() + GetConnector(i);
                }
                name += Values[Values.Count - 1].ToString();
            }

            return name;
        }

        string GetConnector(int i)
        {
            string conncector = " ";
            ConditionsOperator op = ValuesOperators[i];

            switch (op)
            {
                case ConditionsOperator.Or:
                    conncector += "|";
                    break;
                case ConditionsOperator.Greater:
                    conncector += ">";
                    break;
                case ConditionsOperator.GreaterOrEqual:
                    conncector += ">=";
                    break;
                case ConditionsOperator.Less:
                    conncector += "<";
                    break;
                case ConditionsOperator.LessOrEqual:
                    conncector += "<=";
                    break;
                case ConditionsOperator.Equals:
                    conncector += "=";
                    break;
                case ConditionsOperator.Distinct:
                    conncector += "!=";
                    break;
                default:
                    conncector += "|";
                    break;
            }

            conncector += " ";
            return conncector;
        }
    }
}
