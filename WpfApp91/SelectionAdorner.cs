using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Documents;

namespace WpfApp91
{
    class SelectionAdorner : Adorner
    {
        public SelectionAdorner(UIElement adornedElement) : base(adornedElement)
        {
        }

        static readonly Pen _pen = new Pen(Brushes.Black, 1)
        {
            DashStyle = DashStyles.Dash
        };

        static readonly Brush _rectFill = new SolidColorBrush(Color.FromArgb(30, 0, 0, 255));
        static readonly Brush _circleFill = new SolidColorBrush(Color.FromArgb(60, 255, 0, 0));

        const double _circleRadius = 6;


        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawRectangle(_rectFill, _pen, new Rect(AdornedElement.DesiredSize));
            dc.DrawEllipse(_circleFill, null, new Point(0, 0), _circleRadius, _circleRadius);
            dc.DrawEllipse(_circleFill, null, new Point(AdornedElement.DesiredSize.Width, 0),
               _circleRadius, _circleRadius);
            dc.DrawEllipse(_circleFill, null, new Point(AdornedElement.DesiredSize.Width,
                AdornedElement.DesiredSize.Height),
                _circleRadius, _circleRadius);

            dc.DrawEllipse(_circleFill, null, new Point(0, AdornedElement.DesiredSize.Height), _circleRadius, _circleRadius);
        }
    }
}
