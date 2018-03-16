using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// VisualMap.xaml 的交互逻辑
    /// </summary>
    public partial class RealVisualMap : UserControl
    {
        public ObservableCollection<Point> OriginalPoints = new ObservableCollection<Point>();
        private PlaceXmlModel.Item examItem;
        private Point defualtCenterPoint = new Point();
        double _minX = double.MaxValue;
        double _maxX = double.MinValue;
        double _minY = double.MaxValue;
        double _maxY = double.MinValue;

        public RealVisualMap()
        {
            InitializeComponent();
            OriginalPoints.CollectionChanged += OriginalPoints_CollectionChanged;
            Plot1.MouseMove += Plot1OnMouseMove;
            Chart1.SizeChanged += Chart1_SizeChanged;
            FitPlot();
        }

        public void GenerateItem(IExamItemGenerator itemGenerator)
        {
            examItem = itemGenerator.Generate(OriginalPoints);
            foreach (var area in examItem.SubAreas.Areas)
            {
                DrawArea(Plot1,Brushes.Blue, ConvertPointsCollection(area.Points));              
            }
        }

        public void AdjustAxis()
        {
//            FitPlot();
            Chart1.PlotWidth = Chart1.ActualWidth / Chart1.ActualHeight * Chart1.PlotHeight;
        }

        private void Chart1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var chart = sender as Chart;
            chart.PlotWidth = e.NewSize.Width / e.NewSize.Height * chart.PlotHeight;
        }

        private void Plot1OnMouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(sender as IInputElement);
            var x = Plot1.XFromLeft(p.X);
            var y = Plot1.YFromTop(p.Y);
            coordinateTB.Text = $"{x},{y}";
        }

        private void OriginalPoints_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                FitPlot((Point)e.NewItems[0]);
                DrawMapPoints(Plot1, (Point)e.NewItems[0]);
            }           
        }

        private void FitPlot(Point newPoint)
        {
            double maxX = Math.Max(_maxX, newPoint.X);
            double minX = Math.Min(_minX, newPoint.X);
            double maxY = Math.Max(_maxY, newPoint.Y);
            double minY = Math.Min(_minY, newPoint.Y);

            if (maxX != _maxX || minX != _minX || maxY != _maxY || minY != _minY)
            {
                _maxX = maxX;
                _minX = minX;
                _maxY = maxY;
                _minY = minY;

                defualtCenterPoint.X = Math.Round((_maxX + _minX) / 2, 3);
                defualtCenterPoint.Y = Math.Round((_maxY + _minY) / 2, 3);

                Chart1.PlotOriginX = Math.Round(_minX) - 3;
                Chart1.PlotOriginY = Math.Round(_minY) - 3;
                double height = _maxY - _minY + 12;
                Chart1.PlotHeight = height;
                Chart1.PlotWidth = this.Width / this.Height * height;
            }            
        }

        private void FitPlot()
        {
            if(OriginalPoints.Count == 0) return;            
            foreach (var p in OriginalPoints)
            {
                _maxX = Math.Max(_maxX, p.X);
                _minX = Math.Min(_minX, p.X);
                _maxY = Math.Max(_maxY, p.Y);
                _minY = Math.Min(_minY,p.Y);
            }
            defualtCenterPoint.X = Math.Round((_maxX + _minX) / 2, 3);
            defualtCenterPoint.Y = Math.Round((_maxY + _minY) / 2, 3);

            Chart1.PlotOriginX = Math.Round(_minX) - 1;
            Chart1.PlotOriginY = Math.Round(_minY) - 1;
            double height = _maxY - _minY + 2;
            Chart1.PlotHeight = height;
            Chart1.PlotWidth = this.Width / this.Height * height;           
        }

        private Point[] ConvertPointsCollection(IEnumerable<PlaceXmlModel.Point> Placepoints)
        {
            Point[] points = new Point[Placepoints.Count()];
            int i = 0;
            foreach (var p in Placepoints)
            {
                points[i] = new Point(double.Parse(p.X), double.Parse(p.Y));
                i++;
            }
            return points;
        }

        private void DrawArea(PlotBase canva, Brush color, params Point[] points)
        {
            Polygon polygon = new Polygon();
            PointCollection pc = new PointCollection();
            List<MapPoint> mps = new List<MapPoint>();

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
            canva.Children.Add(polygon);
            //DrawMapPoints(canva, points);
        }

        private void DrawMapPoints(PlotBase canva, params Point[] points)
        {
            double pointR = 15;
            foreach (var p in points)
            {
                MapPoint mp = new MapPoint(p.X, p.Y, pointR);
                canva.Children.Add(mp.Shape);
            }
        }
    }
}
