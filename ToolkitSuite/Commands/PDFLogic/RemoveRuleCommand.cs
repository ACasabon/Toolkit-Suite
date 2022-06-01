using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using ToolkitSuite.Models;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
	public class RemoveRuleCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public RemoveRuleCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
            _lsvm.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            bool can = _lsvm.SelectedRule != null;
            return can && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            //Get selected Rules
            List<Rule> rules = new List<Rule>();
            if (parameter is IEnumerable)
            {
                var enumerator = ((IEnumerable)parameter).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    rules.Add((Rule)enumerator.Current);
                }
            }

            //Erase selected rules
            foreach (var rule in rules)
            {
                _lsvm.Rules.Remove(rule);
            }

            //Display Save Button as Active
            NavigationStore.mwvm.LogicUnsavedChanges = true;
        }

        void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_lsvm.SelectedRule))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
