using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolkitSuite.Models
{
    public class MotorVariable
    {
        public MotorVariable()
        {
            Name = "";
            RulesConditions = new List<RuleCondition>();
            //ConditionsOperators = new List<ConditionsOperator>();
            Changes = false;
        }

        public MotorVariable(string name)
        {
            Name = name;
            RulesConditions = new List<RuleCondition>();
            //ConditionsOperators = new List<ConditionsOperator>();
            Changes = false;
        }

        public MotorVariable(string name, List<RuleCondition> rulesConditions)
        {
            Name = name;
            RulesConditions = rulesConditions;
            Changes = false;
        }

        public MotorVariable(string name, List<RuleCondition> rulesConditions, bool changes)
        {
            Name = name;
            RulesConditions = rulesConditions;
            Changes = changes;
        }

        public string Name { get; set; }
        public List<RuleCondition> RulesConditions { get; set; }
        //public List<ConditionsOperator> ConditionsOperators { get; set; }
        public bool Changes { get; set; }
    }
}
