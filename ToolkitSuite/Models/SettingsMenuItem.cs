using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolkitSuite.Models
{
    public class SettingsMenuItem
    {
        public string Title { get; set; }
        public ObservableCollection<SettingsMenuItem> Items { get; set; }

        public SettingsMenuItem()
        {
            this.Items = new ObservableCollection<SettingsMenuItem>();
        }

        public SettingsMenuItem(string Title)
        {
            this.Title = Title;
            this.Items = new ObservableCollection<SettingsMenuItem>();
        }

        public SettingsMenuItem(string Title, ObservableCollection<SettingsMenuItem> Items)
        {
            this.Title = Title;
            this.Items = Items;
        }
    }
}
