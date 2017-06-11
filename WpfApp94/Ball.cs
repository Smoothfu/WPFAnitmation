using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace WpfApp94
{
    class Ball : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public Environment Environment { get; private set; }
        public Ball(Environment env)
        {
            Environment = env;
        }
        Point _position, _velocity;
        public double X
        {
            get
            {
                return _position.X;
            }
            set
            {
                _position.X = value;
                OnPropertyChanged("X");
            }
        }

        public double Y
        {
            get
            {
                return _position.Y;
            }
            set
            {
                _position.Y = value;
                OnPropertyChanged("Y");
            }
        }

        public double Width { get; set; }
        public double Height { get; set; }

        public Point Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
                OnPropertyChanged("Velocity");
            }
        }

        public void Move(Rect bounds)
        {
            _velocity.Y += Environment.Gravity;
            X += Velocity.X;
            Y += Velocity.Y;
            bool xHit = false, yHit = false;
            if (X < bounds.Left)
            {
                X = bounds.Left;
                xHit = true;
            }

            else if (X > bounds.Right - Width)
            {
                X = bounds.Right - Width;
                xHit = true;
            }

            if (Y < bounds.Top)
            {
                Y = bounds.Top;
                yHit = true;
            }

            else if (Y > bounds.Bottom - Height)
            {
                Y = bounds.Bottom - Height;
                yHit = true;
            }

            if (xHit)
            {
                _velocity.X = -_velocity.X;
                _velocity.X *= Environment.Tranation;
            }

            if(yHit)
            {
                _velocity.Y = _velocity.Y;
                _velocity.Y *= Environment.Tranation;
            }

        }
    }
}
