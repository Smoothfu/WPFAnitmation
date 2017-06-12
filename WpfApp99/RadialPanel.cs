using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WpfApp99
{
    class RadialPanel:Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Size maxSize = Size.Empty;
            foreach(UIElement child in Children)
            {
                child.Measure(availableSize);
                maxSize = new Size(
                    Math.Max(maxSize.Width, child.DesiredSize.Width),
                    Math.Max(maxSize.Height, child.DesiredSize.Height));
            }

            return new Size(double.IsPositiveInfinity(availableSize.Width) ? maxSize.Width * 2 : availableSize.Width,
                double.IsPositiveInfinity(availableSize.Height) ?
                maxSize.Height * 2 : availableSize.Height);
        }

        public double StartAngle
        {
            get
            {
                return (double)GetValue(StartAngleProperty);
            }
            set
            {
                SetValue(StartAngleProperty, value);
            }
        }

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double),
                typeof(RadialPanel), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));


        protected override Size ArrangeOverride(Size finalSize)
        {
            var count = Children.Count;
            if(count>0)
            {
                Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);
                double step = 360 / count;
                int index = 0;

                foreach(UIElement element in Children)
                {
                    double angle = StartAngle + step * index++;
                    //reverse default angle increment,shift and convert to radians
                    angle = (90 - angle) * Math.PI / 180;
                    Rect rc = new Rect(new Point(
                        center.X - element.DesiredSize.Width / 2 + (center.X - element.DesiredSize.Width / 2) * Math.Cos(angle),
                        center.Y - element.DesiredSize.Height / 2 - (center.Y - element.DesiredSize.Height / 2) *
                        Math.Sin(angle)), element.DesiredSize);
                    element.Arrange(rc);
                }                
            }
            return finalSize;
        }
    }
}
