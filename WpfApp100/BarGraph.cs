using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WpfApp100
{
    class BarGraph:FrameworkElement
    {
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(double[]), typeof(BarGraph), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
        public double[] Values
        {
            get
            {
                return (double[])GetValue(ValuesProperty);
            }
            set
            {
                SetValue(ValuesProperty, value);
            }
        }

        public static readonly DependencyProperty FillProperty =
            Shape.FillProperty.AddOwner(typeof(BarGraph),
                new FrameworkPropertyMetadata(Brushes.White,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty =
            Shape.StrokeProperty.AddOwner(typeof(BarGraph),
                new FrameworkPropertyMetadata(Brushes.Black,
                    FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner
            (typeof(BarGraph), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Fill
        {
            get
            {
                return (Brush)GetValue(FillProperty);
            }
            set
            {
                SetValue(FillProperty, value);
            }
        }

        public Brush Stroke
        {
            get
            {
                return (Brush)GetValue(StrokeProperty);
            }
            set
            {
                SetValue(StrokeProperty, value);
            }
        }

        public double StrokeThickness
        {
            get
            {
                return (double)GetValue(StrokeThicknessProperty);
            }
            set
            {
                SetValue(StrokeThicknessProperty, value);
            }
        }

        public bool ShowAverage
        {
            get
            {
                return (bool)GetValue(ShowAverageProperty);
            }
            set
            {
                SetValue(ShowAverageProperty, value);
            }
        }

        public static readonly DependencyProperty ShowAverageProperty = DependencyProperty.Register(
            "ShowAverage", typeof(bool), typeof(BarGraph), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));


        protected override void OnRender(DrawingContext dc)
        {
           if(Values==null || Values.Length==0)
            {
                return;
            }

            double max = Values.Max();
            var pen = new Pen(Stroke, StrokeThickness);
            var barSize = ActualWidth / Values.Length;
            for(int i=0;i<Values.Length;i++)
            {
                dc.DrawRectangle(Fill, pen, new Rect(
                    new Point(i * barSize, ActualHeight - Values[i] * ActualHeight / max),
                    new Point((i + 1) * barSize, ActualHeight)));
            }

            if(ShowAverage)
            {
                var avg = ActualHeight - Values.Average() * ActualHeight / max;
                dc.DrawLine(pen, new Point(0, avg), new Point(ActualWidth, avg));
            }
        }
    }
}
