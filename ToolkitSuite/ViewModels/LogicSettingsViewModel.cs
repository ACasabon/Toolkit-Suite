using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToolkitSuite.Commands.PDFLogic;
using ToolkitSuite.Models;

namespace ToolkitSuite.ViewModels
{
	public class LogicSettingsViewModel : ViewModelBase
    {
        public string OpenLogicFilePath;
        public ObservableCollection<MotorVariable> MotorVariables => GetMotorVariables();
        public ObservableCollection<SettingsVariable> ConditionVariables => GetConditionVariables();

        int _listFocus;
        Rule _selectedRule;
        int _selectedRuleIndex;
        RuleCondition _selectedRuleCondition;
        int _selectedRuleConditionIndex;
        MotorVariable _selectedMotorVariable;
        LogicItemUI _selectedItemRule;
        int _selectedItemIndex;
        SettingsVariable _selectedConditionVariable;
        LogicItemUI _selectedGlobalVariable;

        ObservableCollection<Rule> _rules;
        ObservableCollection<LogicItemUI> _selectedItemRules;
        ObservableCollection<LogicItemUI> _globarVariables;


        public int ListFocus
        {
            get { return _listFocus; }
            set
            {
                _listFocus = value;
                OnPropertyChanged(nameof(ListFocus));
                OnPropertyChanged(nameof(RuleEditorTitle));
                GetSelectedItemRules();
            }
        }

        public ObservableCollection<Rule> Rules
        {
            get { return _rules; }
            set
            {
                _rules = value;
                OnPropertyChanged(nameof(Rules));
            }
        }

        public Rule SelectedRule
        {
            get { return _selectedRule; }
            set
            {
                _selectedRule = value;
                _selectedRuleCondition = null;
                OnPropertyChanged(nameof(SelectedRule));
                OnPropertyChanged(nameof(MotorVariables));
                OnPropertyChanged(nameof(ConditionVariables));
                UpdateRuleEditor();
            }
        }

        public int SelectedRuleIndex
        {
            get { return _selectedRuleIndex; }
            set
            {
                _selectedRuleIndex = value;
                OnPropertyChanged(nameof(SelectedRuleIndex));
            }
        }

        public MotorVariable SelectedMotorVariable
        {
            get { return _selectedMotorVariable; }
            set
            {
                _selectedMotorVariable = value;
                OnPropertyChanged(nameof(SelectedMotorVariable));
                OnPropertyChanged(nameof(ConditionVariables));
                UpdateRuleEditor();
            }
        }


        public ObservableCollection<LogicItemUI> SelectedItemRules
        {
            get { return _selectedItemRules; }
            set
            {
                _selectedItemRules = value;
                OnPropertyChanged(nameof(SelectedItemRules));
            }
        }

        public ObservableCollection<LogicItemUI> GlobalVariables
        {
            get { return _globarVariables; }
            set
            {
                _globarVariables = value;
                OnPropertyChanged(nameof(GlobalVariables));
            }
        }

        public LogicItemUI SelectedGlobalVariable
        {
            get { return _selectedGlobalVariable; }
            set
            {
                _selectedGlobalVariable = value;
                OnPropertyChanged(nameof(SelectedGlobalVariable));
            }
        }

        public RuleCondition SelectedRuleCondition
        {
            get { return _selectedRuleCondition; }
            set
            {
                _selectedRuleCondition = value;
                OnPropertyChanged(nameof(SelectedRuleCondition));
                OnPropertyChanged(nameof(ConditionVariables));
                GetSelectedItemRules();
            }
        }

        public int SelectedRuleConditionIndex
        {
            get { return _selectedRuleConditionIndex; }
            set
            {
                _selectedRuleConditionIndex = value;
                OnPropertyChanged(nameof(SelectedRuleConditionIndex));
            }
        }

        public LogicItemUI SelectedItemRule
        {
            get { return _selectedItemRule; }
            set
            {
                _selectedItemRule = value;
                OnPropertyChanged(nameof(SelectedItemRule));
            }
        }

        public int SelectedItemIndex
        {
            get { return _selectedItemIndex; }
            set
            {
                _selectedItemIndex = value;
                OnPropertyChanged(nameof(SelectedItemIndex));
            }
        }

        public SettingsVariable SelectedConditionVariable
        {
            get { return _selectedConditionVariable; }
            set
            {
                _selectedConditionVariable = value;
                OnPropertyChanged(nameof(SelectedConditionVariable));
            }
        }

        public string RuleEditorTitle => GetRuleEditorTitle();

        public ICommand AddRuleCommand { get; }
        public ICommand RemoveRuleCommand { get; }
        public ICommand AddRuleConditionCommand { get; }
        public ICommand RemoveRuleConditionCommand { get; }
        public ICommand AddSettingsVariableCommand { get; }
        public ICommand RemoveSettingsVariableCommand { get; }
        public ICommand AddRuleEditorValueCommand { get; }
        public ICommand RemoveRuleEditorValueCommand { get; }
        public ICommand SaveRuleEditorValuesCommand { get; }
        public ICommand AddGlobalVariableCommand { get; }
        public ICommand RemoveGlobalVariableCommand { get; }

        public LogicSettingsViewModel()
        {
            AddRuleCommand = new AddRuleCommand(this);
            RemoveRuleCommand = new RemoveRuleCommand(this);
            AddRuleConditionCommand = new AddRuleConditionCommand(this);
            RemoveRuleConditionCommand = new RemoveRuleConditionCommand(this);
            AddSettingsVariableCommand = new AddSettingsVariableCommand(this);
            RemoveSettingsVariableCommand = new RemoveSettingsVariableCommand(this);
            AddRuleEditorValueCommand = new AddRuleEditorValueCommand(this);
            RemoveRuleEditorValueCommand = new RemoveRuleEditorValueCommand(this);
            SaveRuleEditorValuesCommand = new SaveRuleEditorValuesCommand(this);
            AddGlobalVariableCommand = new AddGlobalVariableCommand(this);
            RemoveGlobalVariableCommand = new RemoveGlobalVariableCommand(this);
            //_globarVariables = new ObservableCollection<LogicItemUI>();
            _globarVariables = new ObservableCollection<LogicItemUI>() { new LogicItemUI("REGISTER ID", true), new LogicItemUI("SERIAL NUMBER", true), new LogicItemUI("CONTROL BOX VERSION", true) };
        }

        public void NewRuleEditorValue()
        {
            GetSelectedItemRules();
        }

        public void CheckForChanges()
        {
            RuleCondition selRC = SelectedRuleCondition;
            OnPropertyChanged(nameof(Rules));
            OnPropertyChanged(nameof(SelectedRule));
            OnPropertyChanged(nameof(MotorVariables));
            OnPropertyChanged(nameof(ConditionVariables));
            GetSelectedItemRules();
            OnPropertyChanged(nameof(SelectedItemRule));
            SelectedRuleCondition = selRC;
            OnPropertyChanged(nameof(SelectedRuleCondition));
        }        

        public void UpdateRuleEditor()
        {
            OnPropertyChanged(nameof(RuleEditorTitle));
            GetSelectedItemRules();
            OnPropertyChanged(nameof(SelectedItemRule));
        }

        string GetRuleEditorTitle()
        {
            string title = "";

            if(_listFocus == 0)
                title = "MOTOR CONFIGURATION PARAMETERS";
            else
                title = "PARAMETERS VALUES";

            return title;
        }

        ObservableCollection<MotorVariable> GetMotorVariables()
		{
            if (SelectedRule is null)
                return new ObservableCollection<MotorVariable>();
            return new ObservableCollection<MotorVariable>(SelectedRule.MotorVariables);
        }

        ObservableCollection<SettingsVariable> GetConditionVariables()
		{
            if (SelectedRuleCondition is null)
                return new ObservableCollection<SettingsVariable>();
            return new ObservableCollection<SettingsVariable>(SelectedRuleCondition.Variables);
        }

        void GetSelectedItemRules()
        {
            _selectedItemRules = new ObservableCollection<LogicItemUI>();

            if (_listFocus == 0)
            {
                _selectedItemRules = GetSelectedRuleMotorVariables();
            }
            else
            {
                _selectedItemRules = GetSelectedRuleParameters();
            }

            OnPropertyChanged(nameof(SelectedItemRules));
        }

        ObservableCollection<LogicItemUI> GetSelectedRuleMotorVariables()
        {
            var selectedItemRules = new ObservableCollection<LogicItemUI>();

            if (SelectedRule == null)
                return selectedItemRules;
            int motorVarsCount = SelectedRule.MotorVariables.Count;

            if (motorVarsCount <= 0)
                return selectedItemRules;

            //Operators to List<string>
            List<string> operators = new List<string>();
            foreach (MotorVarsOperator motorVarOperator in (MotorVarsOperator[])Enum.GetValues(typeof(MotorVarsOperator)))
                operators.Add(motorVarOperator.ToString());

            for (int i = 0; i < SelectedRule.MotorVariables.Count; i++)
            {
                selectedItemRules.Add(new LogicItemUI(SelectedRule.MotorVariables[i].Name));
                if (i != motorVarsCount - 1)
                {
                    string selected = SelectedRule.MotorVarsOperators[i].ToString();
                    selectedItemRules.Add(new LogicItemUI(operators, selected));
                }
            }

            return selectedItemRules;
        }

        ObservableCollection<LogicItemUI> GetSelectedRuleParameters()
        {
            var selectedItemRules = new ObservableCollection<LogicItemUI>();

            if (SelectedRuleCondition == null)
                return selectedItemRules;
            int ruleConditonsCount = SelectedRuleCondition.Values.Count;

            if (ruleConditonsCount <= 0)
                return selectedItemRules;

            //Operators to List<string>
            List<string> operators = new List<string>();
            foreach (ConditionsOperator conditionOperator in (ConditionsOperator[])Enum.GetValues(typeof(ConditionsOperator)))
                operators.Add(conditionOperator.ToString());

            for (int i = 0; i < SelectedRuleCondition.Values.Count; i++)
            {
                selectedItemRules.Add(new LogicItemUI(SelectedRuleCondition.Values[i].ToString()));
                if (i != ruleConditonsCount - 1)
                {
                    string selected = SelectedRuleCondition.ValuesOperators[i].ToString();
                    selectedItemRules.Add(new LogicItemUI(operators, selected));
                }
            }

            return selectedItemRules;
        }
    }
}
