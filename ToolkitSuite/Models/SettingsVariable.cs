using Newtonsoft.Json;
using System;
using System.ComponentModel;
using ToolkitSuite.Stores;

namespace ToolkitSuite.Models
{
    public class SettingsVariable : INotifyPropertyChanged
    {
        object _value;
        VariableType _type;

        public string FullName
        {
            get
            {
                string name = string.IsNullOrEmpty(Subgroup) ? Group + "." + Name : Group + "." + Subgroup + "." + Name;
                return name;
            }
            set
            {
                string[] parts = value.Split('.');
                Group = parts[0];
                if(parts.Length > 2)
                {
                    Subgroup = parts[1];
                    Name = parts[2];
                }
                else
                {
                    Subgroup = null;
                    Name = parts[1];
                }
            }
        }
        public string Name { get; set; }

        //public object Value { get; set; }

        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public string SelectedValue { get; set; }
        public string Group { get; set; }
        public string Subgroup { get; set; }
        public bool Changes { get; set; }

        //public VariableType Type { get; set; }
        public VariableType Type
        {
            get { return _type; }
            set
            {
                ChangeValue(value);
                _type = value;
                OnPropertyChanged(nameof(Type));
                OnPropertyChanged(nameof(Value));
                NavigationStore.LogicSettingsViewModel.CheckForChanges();
            }
        }
        public VariableType[] Types => (VariableType[])Enum.GetValues(typeof(VariableType));

        public enum VariableType
        {
            Value,
            Bool,
            GlobalVariable
        }
        
        public SettingsVariable(string name, object value)
        {
            Name = name;
            Value = value;
            Type = GetVarType();
        }

        public SettingsVariable(string name, object value, string selectedValue)
        {
            Name = name;
            Value = value;
            SelectedValue = selectedValue;
            Type = GetVarType();
        }

        public SettingsVariable(string name, object value, string group, string subgroup)
        {
            Name = name;
            Value = value;
            Group = group;
            Subgroup = subgroup;
            Type = GetVarType();
        }

        public SettingsVariable(string name, object value, string selectedValue, string group, string subgroup)
        {
            Name = name;
            Value = value;
            SelectedValue = selectedValue;
            Group = group;
            Subgroup = subgroup;
            Type = GetVarType();
        }

        [JsonConstructor]
        public SettingsVariable(string name, object value, string selectedValue, string group, string subgroup, bool changes)
        {
            Name = name;
            Value = value;
            SelectedValue = selectedValue;
            Group = group;
            Subgroup = subgroup;
            Changes = changes;
            Type = GetVarType();
        }

        VariableType GetVarType()
        {
            VariableType type = VariableType.Value;

            string valueStr = Value.ToString().ToLower();
            if (Value is bool || valueStr == "true" || valueStr == "false")
                type = VariableType.Bool;

            return type;
        }

        void ChangeValue(VariableType newType)
        {
            if(newType != _type)
            {
                switch (newType)
                {
                    case VariableType.Value:
                        Value = "";
                        break;
                    case VariableType.Bool:
                        Value = true;
                        break;
                    case VariableType.GlobalVariable:
                        Value = 0;
                        break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}