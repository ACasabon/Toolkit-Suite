using ToolkitSuite.Models;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.PDFLogic
{
	public class AddGlobalVariableCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public AddGlobalVariableCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
        }

        public override void Execute(object parameter)
        {
            _lsvm.GlobalVariables.Add(new LogicItemUI(""));

            //Display Save Button as Active
            NavigationStore.mwvm.LogicUnsavedChanges = true;
        }
    }
}
