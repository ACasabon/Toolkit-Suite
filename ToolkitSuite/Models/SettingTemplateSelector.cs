using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ToolkitSuite.Models
{
    public class SettingsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CheckBoxTemplate { get; set; }
        public DataTemplate TextBoxTemplate { get; set; }
        public DataTemplate ComboBoxTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            object value = null;
            if(item is SettingsVariable)
            {
                SettingsVariable var = item as SettingsVariable;
                switch (var.Type)
                {
                    case SettingsVariable.VariableType.Value:
                        value = var.Value;
                        break;
                    case SettingsVariable.VariableType.Bool:
                        return CheckBoxTemplate;
                    case SettingsVariable.VariableType.GlobalVariable:
                        return ComboBoxTemplate;
                }
            }
            else if (item is LogicItemUI)
            {
                LogicItemUI var = item as LogicItemUI;
                value = var.Value;
            }

            if (value is bool)
            {
                return CheckBoxTemplate;
            }
            else if (value is List<string>)
            {
                return ComboBoxTemplate;
            }
            return TextBoxTemplate;
        }
    }
}
