using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolkitSuite.Models
{
    public class LogicItemUI : INotifyPropertyChanged
    {
        object _value;
        object _selectedValue;
        bool _enabled;

        public object Value {
            get {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public object SelectedValue
        {
            get
            {
                return _selectedValue;
            }
            set
            {
                _selectedValue = value;
                OnPropertyChanged(nameof(SelectedValue));
            }
        }

        public bool ReadOnly
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                OnPropertyChanged(nameof(ReadOnly));
            }
        }

        public LogicItemUI(object value, bool readOnly = false)
        {
            Value = value;
            ReadOnly = readOnly;
        }

        public LogicItemUI(object value, object selectedValue, bool readOnly = false)
        {
            Value = value;
            SelectedValue = selectedValue;
            ReadOnly = readOnly;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
