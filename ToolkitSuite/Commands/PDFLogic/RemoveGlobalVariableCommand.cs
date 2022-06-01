using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using ToolkitSuite.Models;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
	public class RemoveGlobalVariableCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public RemoveGlobalVariableCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
            _lsvm.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            bool selected = _lsvm.SelectedGlobalVariable != null;
            bool readOnly = false;
            if(selected)
                 readOnly = _lsvm.SelectedGlobalVariable.ReadOnly;
            return selected && !readOnly && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            List<LogicItemUI> globalVariables = new List<LogicItemUI>();
            if (parameter is IEnumerable)
            {
                var enumerator = ((IEnumerable)parameter).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    globalVariables.Add((LogicItemUI)enumerator.Current);
                }
            }

            //Erase selected rules
            foreach (var variable in globalVariables)
            {
                _lsvm.GlobalVariables.Remove(variable);
            }

            //Display Save Button as Active
            NavigationStore.mwvm.LogicUnsavedChanges = true;
        }

        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_lsvm.SelectedGlobalVariable))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
