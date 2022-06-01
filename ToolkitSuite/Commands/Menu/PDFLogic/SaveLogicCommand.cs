using Newtonsoft.Json;
using System.IO;
using ToolkitSuite.Stores;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.Menu.PDFLogic
{
	public class SaveLogicCommand : CommandBase
    {
        LogicSettingsViewModel _lsvm;

        public SaveLogicCommand(LogicSettingsViewModel lsvm)
        {
            _lsvm = lsvm;
        }

        public override void Execute(object parameter)
        {
            if (!NavigationStore.mwvm.LogicUnsavedChanges)
                return;
            string path = _lsvm.OpenLogicFilePath;
            if (string.IsNullOrEmpty(path))
                return;

            //Convert rules to JSON
            string json = JsonConvert.SerializeObject(_lsvm.Rules);
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(json);
            }

            //Display Save Button as Unactive
            NavigationStore.mwvm.LogicUnsavedChanges = false;
        }
    }
}
