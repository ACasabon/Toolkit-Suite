using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using ToolkitSuite.Models;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
    class RemoveSettingsVariableCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public RemoveSettingsVariableCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
            _lsvm.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            bool can = _lsvm.SelectedConditionVariable != null;
            return can && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            //Get selected Variables
            List<SettingsVariable> variables = new List<SettingsVariable>();
            if (parameter is IEnumerable)
            {
                var enumerator = ((IEnumerable)parameter).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    variables.Add((SettingsVariable)enumerator.Current);
                }
            }

            //Erase selected rules
            foreach (var var in variables)
            {
                _lsvm.SelectedRuleCondition.Variables.Remove(var);
            }

            _lsvm.CheckForChanges();

            //Display Save Button as Active
            NavigationStore.mwvm.LogicUnsavedChanges = true;
        }

        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_lsvm.SelectedConditionVariable))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
