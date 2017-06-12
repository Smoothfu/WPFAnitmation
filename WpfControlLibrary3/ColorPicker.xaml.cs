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
using System.Globalization;

namespace WpfControlLibrary3
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public ColorPicker()
        {
            InitializeComponent();
        }

        static ColorPicker()
        {
            CommandManager.RegisterClassCommandBinding(typeof(ColorPicker), new CommandBinding(
                MediaCommands.ChannelUp, ChannelUpExecute, ChannelUpCanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(ColorPicker), new CommandBinding(MediaCommands.ChannelDown,
                ChannelDownExecute, ChannelDownCanExecute));
        }

        static void ChannelUpExecute(object sender,ExecutedRoutedEventArgs e)
        {
            var cp = (ColorPicker)sender;
            var color = cp.SelectedColor;
            if (color.R < 255) color.R++;
            if (color.G < 255) color.G++;
            if (color.B < 255) color.B++;
            cp.SelectedColor = color;
        }

        static void ChannelUpCanExecute(object sender,CanExecuteRoutedEventArgs e)
        {
            var color = ((ColorPicker)sender).SelectedColor;
            e.CanExecute = color.R < 255 || color.G < 255 || color.B < 255;
        }

        static void ChannelDownExecute(object sender,ExecutedRoutedEventArgs e)
        {
            var cp = (ColorPicker)sender;
            var color = cp.SelectedColor;
            if (color.R > 0) color.R--;
            if (color.G > 0) color.G--;
            if (color.B > 0) color.B--;
            cp.SelectedColor = color;
        }

        static void ChannelDownCanExecute(object sender,CanExecuteRoutedEventArgs e)
        {
            var color = ((ColorPicker)sender).SelectedColor;
            e.CanExecute = color.R > 0 || color.G > 0 || color.B > 0;
        }
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
            "SelectedColor", typeof(Color), typeof(ColorPicker),
            new UIPropertyMetadata(Colors.Black, OnSelectedColorChanged));

        public Color SelectedColor
        {
            get
            {
                return (Color)GetValue(SelectedColorProperty);
            }
            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }

        public static RoutedEvent SelectedColorChangedEvent =
            EventManager.RegisterRoutedEvent("SelectedColorChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Color>),
                typeof(ColorPicker));

        public event RoutedPropertyChangedEventHandler<Color> SelectedColorChanged
        {
            add
            {
                AddHandler(SelectedColorChangedEvent, value);
            }
            remove
            {
                RemoveHandler(SelectedColorChangedEvent, value);
            }
        }

        static void OnSelectedColorChanged(DependencyObject obj,DependencyPropertyChangedEventArgs e)
        {
            var cp = (ColorPicker)obj;
            cp.RaiseEvent(new RoutedPropertyChangedEventArgs<Color>(
                (Color)e.OldValue, (Color)e.NewValue, SelectedColorChangedEvent));
        }
    }

    class ColorToDoubleConverter : IValueConverter
    {
        private Color _lastColor;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;
            _lastColor = color;
            switch((string)parameter)
            {
                case "r":
                    return color.R;
                case "g":
                    return color.G;
                case "b":
                    return color.B;
                case "a":
                    return color.A; 
            }
            return Binding.DoNothing;
        }

       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = _lastColor;
            var intensity = (byte)(double)value;
            switch((string)parameter)
            {
                case "r":
                    color.R = intensity;
                    break;
                case "g":
                    color.G = intensity;
                    break;
                case "b":
                    color.B = intensity;
                    break;
                case "a":
                    color.A = intensity;
                    break;
            }
            _lastColor = color;
            return color;
        }      
    }

    class ColorToBrushConverter : IValueConverter
    {
        SolidColorBrush _red = new SolidColorBrush(),
          _green = new SolidColorBrush(),
          _blue = new SolidColorBrush(),
          _alpha = new SolidColorBrush();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;
            switch((string)parameter)
            {
                case "r":
                    _red.Color = Color.FromRgb(color.R, 0, 0);
                    return _red;
                case "g":
                    _green.Color = Color.FromRgb(0, color.G, 0);
                    return _green;
                case "b":
                    _blue.Color = Color.FromRgb(0, 0, color.B);
                    return _blue;
                case "a":
                    _alpha.Color = Color.FromArgb(color.A, 128, 128, 128);
                    return _alpha;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
