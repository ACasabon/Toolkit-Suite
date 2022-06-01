using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolkitSuite.Models;
using ToolkitSuite.Stores;

namespace ToolkitSuite.Views
{
    /// <summary>
    /// Interaction logic for LogicSettingsView.xaml
    /// </summary>
    public partial class LogicSettingsView : UserControl
    {
        public LogicSettingsView()
        {
            InitializeComponent();
        }

        private void VariablesListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            double eraseColWidth = 35;
            double workingWidth = listView.ActualWidth - eraseColWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            double col1 = 0.5;
            double col2 = 0.3;
            double col3 = 0.2;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
        }

        private void EditorListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            double eraseColWidth = 35;
            var workingWidth = listView.ActualWidth - eraseColWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 1;

            gView.Columns[0].Width = workingWidth * col1;
        }

        private void RulesListView_GotFocus(object sender, RoutedEventArgs e)
        {
            NavigationStore.LogicSettingsViewModel.ListFocus = 0;
        }

        private void ParametersListView_GotFocus(object sender, RoutedEventArgs e)
        {
            NavigationStore.LogicSettingsViewModel.ListFocus = 1;
        }

        private void ConditionsListView_GotFocus(object sender, RoutedEventArgs e)
        {
            ListView lv = (ListView)sender;
            RuleCondition rc = (RuleCondition)lv.SelectedItem;
            if(rc != null)
                NavigationStore.LogicSettingsViewModel.SelectedRuleCondition = rc;
        }
    }
}
