using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace WpfApp94
{
    class Environment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public static double Tranation = 0.95;
        double _gravity;
        public double Gravity
        {
            get
            {
                return _gravity;
            }
            set
            {
                _gravity = value;
                OnPropertyChanged("Gravity");
            }
        }
    }
}
