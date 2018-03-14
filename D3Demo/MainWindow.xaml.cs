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
using InteractiveDataDisplay.WPF;

namespace D3Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //0
            DrawArea(Brushes.Red, new Point(14190.491, 6742.325), new Point(14183.954, 6743.655), new Point(14184.054, 6744.145), new Point(14190.591, 6742.815));
            //1
            DrawArea(Brushes.Blue, new Point(14184.054, 6744.145), new Point(14186.744, 6757.991), new Point(14193.375, 6756.815), new Point(14190.591, 6742.815));
            //2
            DrawArea(Brushes.Red, new Point(14186.744, 6757.991), new Point(14186.866, 6758.680), new Point(14193.497, 6757.504), new Point(14193.375, 6756.815));
            //3
            DrawArea(Brushes.Blue, new Point(14191.617, 6747.879), new Point(14192.077, 6750.163), new Point(14197.146, 6749.081), new Point(14196.705, 6746.835));



            Chart1.PlotOriginX = 14180;
            Chart1.PlotOriginY = 6740;
            double height = 20;
            Chart1.PlotHeight = height;
            Chart1.PlotWidth = this.Width / this.Height * height;
            Chart1.SizeChanged += Chart1_SizeChanged;

            //Plot1.Children.OfType<MouseNavigation>().ToList()[0]
            //mouse.MouseMove += Mouse_MouseMove;
        }

        private void Chart1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Chart1.PlotWidth = e.NewSize.Width / e.NewSize.Height * Chart1.PlotHeight;
        }

        private void DrawArea(Brush color,params Point[] points)
        {
            Polygon polygon = new Polygon();
            PointCollection pc = new PointCollection();
            List<MapPoint> mps = new List<MapPoint>();
            double pointR = 15;
            
            foreach (var p in points)
            {
                pc.Add(p);
                //mps.Add(new MapPoint(p.X, p.Y, pointR));
                //MapPoint mp = new MapPoint(p.X,p.Y, pointR);
                //Plot1.Children.Add(mp.Shape);
            }

            polygon.Stroke = color;
            polygon.StrokeThickness = 2;

            

            Plot.SetPoints(polygon, pc);
            Plot1.Children.Add(polygon);
            DrawMapPoints(points);
        }

        private void DrawMapPoints(IEnumerable<Point> points)
        {
            double pointR = 15;
            foreach (var p in points)
            {
                MapPoint mp = new MapPoint(p.X, p.Y, pointR);
                Plot1.Children.Add(mp.Shape);
            }
        }

        private void Chart1_Loaded(object sender, RoutedEventArgs e)
        {
            //double height = 20;
            //Chart1.PlotHeight = height;
            //Chart1.PlotWidth = Chart1.Width / Chart1.Height * height;
            //Chart1.SizeChanged += Chart1_SizeChanged;
        }

        private void Plot1_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(sender as IInputElement);
            var x = Plot1.XFromLeft(p.X);
            var y = Plot1.YFromTop(p.Y);
            coordinateTB.Text = $"{x},{y}";
            Console.WriteLine($"{x},{y}");
        }

        //private void MouseNavigation_MouseMove(object sender, MouseEventArgs e)
        //{

        //}
    }
}
