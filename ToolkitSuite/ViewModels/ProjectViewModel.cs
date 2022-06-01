using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToolkitSuite.Models;

namespace ToolkitSuite.ViewModels
{
	public class ProjectViewModel : ViewModelBase
    {
        public ObservableCollection<SettingsMenuItem> SettingsMenuItems => GetSettingsMenuItems();
        public ObservableCollection<SettingsVariable> SettingsVariables => GetSettingsVariables();

        SettingsVariable _selectedVariable;

        public SettingsVariable SelectedVariable
        {
            get { return _selectedVariable; }
            set
            {
                _selectedVariable = value;
                OnPropertyChanged(nameof(SelectedVariable));
            }
        }

        public ProjectViewModel()
        {
        }

        ObservableCollection<SettingsMenuItem> GetSettingsMenuItems()
        {
            var settingsMenuItems = new ObservableCollection<SettingsMenuItem>();

            var settingsGroups = GetSettingsGroups();
            foreach (var group in settingsGroups)
            {
                var groupItems = new ObservableCollection<SettingsMenuItem>();
                foreach (var subgroup in group.Subgroups)
                {
                    groupItems.Add(new SettingsMenuItem(subgroup.Name));
                }
                SettingsMenuItem menuItem = new SettingsMenuItem(group.Name, groupItems);
                settingsMenuItems.Add(menuItem);
            }

            return settingsMenuItems;
        }

        List<SettingsGroup> GetSettingsGroups()
        {
            //Replace for real data

            var settingsGroups = new List<SettingsGroup>();

            SettingsVariable sv = new SettingsVariable("Var", 1, "Group", "SubG");
            var svl = new List<SettingsVariable>() { sv };
            SettingsSubgroup ssg = new SettingsSubgroup("SubG", svl);
            var ssgl = new List<SettingsSubgroup>() { ssg };
            SettingsGroup sg = new SettingsGroup("Group", ssgl, svl);

            settingsGroups.Add(sg);
            settingsGroups.Add(sg);
            settingsGroups.Add(sg);
            settingsGroups.Add(sg);

            return settingsGroups;
        }

        ObservableCollection<SettingsVariable> GetSettingsVariables()
        {
            var settingsVariables = new ObservableCollection<SettingsVariable>();

            SettingsVariable sv = new SettingsVariable("VarValue", 1, "Group", "SubG");
            settingsVariables.Add(sv);
            SettingsVariable sv2 = new SettingsVariable("VarBool", true, "Group", "SubG");
            settingsVariables.Add(sv2);
            List<string> svl = new List<string>()
            {
                "Option1",
                "Option2",
                "Option3",
                "Option4"
            };
            SettingsVariable sv3 = new SettingsVariable("VarCombo", svl, "Option3");
            settingsVariables.Add(sv3);
            SettingsVariable sv4 = new SettingsVariable("VarCombo2", svl, "Option2");
            settingsVariables.Add(sv4);

            return settingsVariables;
        }
    }
}
