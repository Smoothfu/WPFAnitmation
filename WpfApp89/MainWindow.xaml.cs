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

namespace WpfApp89
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point _current;
        FrameworkElement _currentShape;
        bool _moving;
        Adorner _adorner;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += delegate
            {
                CreateCircles();
            };
        }

        void CreateCircles()
        {
            var rnd = new Random();
            int start = rnd.Next(30);
            for(int i=0;i<10;i++)
            {
                var circle = new Ellipse
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Width = 50,
                    Height = 50
                };

                var fill = typeof(Brushes).GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)[start].
                    GetValue(null, null) as Brush;
                circle.Fill = fill;
                Canvas.SetLeft(circle, rnd.NextDouble() * ActualWidth);
                Canvas.SetTop(circle, rnd.NextDouble() * ActualHeight);
                _canvas.Children.Add(circle);
                start += 2;

            }
        }

        private void _canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var layer = AdornerLayer.GetAdornerLayer(_canvas);
            if(_adorner!=null)
            {
                layer.Remove(_adorner);
                _adorner = null;
            }

            var shape = e.Source as Shape;
            if(shape!=null)
            {
                _moving = true;
                _current = e.GetPosition(_canvas);
                _currentShape = shape;
                _adorner = new SelectionAdorner(shape);
                layer.Add(_adorner);
                _canvas.CaptureMouse();
            }
        }

        private void _canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(_moving)
            {
                _moving = false;
                _canvas.ReleaseMouseCapture();
            }
        }

        private void _canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(_moving)
            {
                var pt = e.GetPosition(_canvas);
                Canvas.SetLeft(_currentShape, Canvas.GetLeft(_currentShape) + pt.X - _current.X);
                Canvas.SetTop(_currentShape, Canvas.GetTop(_currentShape) + pt.Y - _current.Y);
                _current = pt;
            }
        }
    }
}
