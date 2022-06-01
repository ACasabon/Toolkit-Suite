using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolkitSuite.ViewModels;

namespace ToolkitSuite.Commands
{
    public class CloseLogicTabCommand : CommandBase
    {
        MainWindowViewModel _mwvm;

        public CloseLogicTabCommand(MainWindowViewModel mwvm)
        {
            _mwvm = mwvm;
        }

        public override void Execute(object parameter)
        {
            _mwvm.DisplayPDFLogic = false;
            _mwvm.SelectedMenuTab = 2;
            _mwvm.UpdateViewModel();
        }
    }
}
