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
    }
}
