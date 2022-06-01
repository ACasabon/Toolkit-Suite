using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using ToolkitSuite.Helpers;
using ToolkitSuite.Models;
using ToolkitSuite.Properties;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands.Menu.PDFLogic
{
	class ImportLogicCommand : CommandBase
    {
        LogicSettingsViewModel _logicVM;

        public ImportLogicCommand(LogicSettingsViewModel logicVM)
        {
            _logicVM = logicVM;
        }

        public override void Execute(object parameter)
        {
            ImportJson();
        }

        void ImportJson()
        {
            //Convert JSON to rules
            string path = MyFileDialog.OpenFile(FilterType.JSON, Settings.Default.PDFLogicFilesFolder);
            //string path = @"Logic\testImport.json";
            if (string.IsNullOrEmpty(path))
                return;
            string json = File.ReadAllText(path);
            _logicVM.Rules = JsonConvert.DeserializeObject<ObservableCollection<Rule>>(json);
            _logicVM.OpenLogicFilePath = path;
        }
    }
}
