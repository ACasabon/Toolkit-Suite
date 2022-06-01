using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolkitSuite.Models
{
    public class SettingsGroup
    {
        public string Name;
        public List<SettingsSubgroup> Subgroups;
        public List<SettingsVariable> Variables;

        public SettingsGroup(string name, List<SettingsSubgroup> subgroups, List<SettingsVariable> variables)
        {
            this.Name = name;
            this.Subgroups = subgroups;
            this.Variables = variables;
        }
    }
}
