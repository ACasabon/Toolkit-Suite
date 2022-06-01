using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitSuite.ViewModels;
using Newtonsoft.Json;
using System.IO;
using ToolkitSuite.Helpers;

namespace ToolkitSuite.Commands.Menu.PDFLogic
{
    public class ExportLogicCommand : CommandBase
    {
        LogicSettingsViewModel _logicVM;

        public ExportLogicCommand(LogicSettingsViewModel logicVM)
        {
            _logicVM = logicVM;
        }

        public override void Execute(object parameter)
        {
            ExportAsJson();
        }

        void ExportAsJson()
        {
            //Convert JSON to rules
            string path = MyFileDialog.SaveFile(FilterType.JSON);
            //string path = @"Logic\testExport.json";
            if (string.IsNullOrEmpty(path))
                return;

            //Convert rules to JSON
            string json = JsonConvert.SerializeObject(_logicVM.Rules);
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(json);
            }
        }
    }
}
