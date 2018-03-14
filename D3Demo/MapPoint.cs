using InteractiveDataDisplay.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace D3Demo
{
    public class MapPoint
    {
        private Brush _color = Brushes.BlueViolet;
        private double _r = 1;
        /// <summary>
        /// 地图点形状
        /// </summary>
        public Ellipse Shape { get; private set; } = new Ellipse();
        //半径
        public double R
        {
            get
            {
                return _r;
            }
            set
            {
                _r = value;
                Shape.Height = _r;
                Shape.Width = _r;
                //根据新半径重新设置一次坐标
                SetXY(X,Y);
            }
        }

        public double X { get; private set;}

        public double Y { get; private set; }

        public Brush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                Shape.Fill = _color;
                Shape.Stroke = _color;
            }
        }

        public MapPoint(double x, double y, double r)
        {
            _r = r;
            Shape.Height = _r;
            Shape.Width = _r;
            SetXY(x,y);
            Shape.Fill = _color;
            Shape.Stroke = _color;
            Shape.MouseEnter += Shape_MouseEnter;
        }

        private void Shape_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var ellipse = sender as Ellipse;
            var toolTip = new ToolTip();
            TextBlock tb = new TextBlock();
            //var p = (Point)ellipse.Tag;
            tb.Text = $"{X},{Y}";
            toolTip.Content = tb;
            ellipse.ToolTip = toolTip;
        }

        public void SetXY(double x,double y)
        {
            X = x;
            Y = y;
            Plot.SetX1(Shape, X);
            Plot.SetY1(Shape, Y);
            //X,Y设置的是圆点的Left和Top点，平移后，将X,Y表示为圆点的圆心点。
            TranslateTransform t = new TranslateTransform();
            t.X = -_r / 2;
            t.Y = -_r / 2;
            Shape.RenderTransform = t;
        }

    }
}
