using Newtonsoft.Json;
using System.Collections.Generic;

namespace ToolkitSuite.Models
{
    public class Rule
    {
        public Rule(List<MotorVariable> motorVariables)
        {
            MotorVariables = motorVariables;
            MotorVarsOperators = new List<MotorVarsOperator>();
        }

        [JsonConstructor]
        public Rule(List<MotorVariable> motorVariables, List<MotorVarsOperator> motorVarsOperators)
        {
            MotorVariables = motorVariables;
            MotorVarsOperators = motorVarsOperators;
        }

        public string Name => GetRuleName();
        public List<MotorVariable> MotorVariables { get; set; }
        public List<MotorVarsOperator> MotorVarsOperators { get; set; }

        string GetRuleName()
        {
            string name = null;

            if(MotorVariables.Count == 1)
            {
                name = MotorVariables[0].Name;
            }
            else if (MotorVariables.Count > 0)
            {
                for (int i = 0; i < MotorVariables.Count - 1; i++)
                {
                    name += MotorVariables[i].Name + " & ";
                }
                name += MotorVariables[MotorVariables.Count - 1].Name;
            }

            return name;
        }
    }
}
