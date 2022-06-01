using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitSuite.Properties;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands
{
    public class OpenLogicTabCommand : CommandBase
    {
        MainWindowViewModel _mwvm;

        public OpenLogicTabCommand(MainWindowViewModel mwvm)
        {
            _mwvm = mwvm;
        }

        public override void Execute(object parameter)
        {
            _mwvm.DisplayPDFLogic = true;
            _mwvm.SelectedMenuTab = 3;
            _mwvm.UpdateViewModel();
            _mwvm.OpenActiveLogic();
        }
    }
}
