using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml;
using System.Xml.Serialization;
using InteractiveDataDisplay.WPF;

namespace D3Demo
{
    /// <summary>
    /// VisualMap.xaml 的交互逻辑
    /// </summary>
    public partial class RealVisualMap : UserControl
    {
        public ObservableCollection<MapPoint> OriginalPoints = new ObservableCollection<MapPoint>();
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
            examItem = itemGenerator.Generate(OriginalPoints.Select(mp=> new Point(mp.X,mp.Y)));
            foreach (var area in examItem.SubAreas.Areas)
            {
                DrawArea(Plot1,Brushes.Blue, ConvertPointsCollection(area.Points));              
            }
        }

        public void ExportMap()
        {
            PlaceXmlModel map = new PlaceXmlModel();
            map.Items = new List<PlaceXmlModel.Item>();
            map.Items.Add(examItem);
            map.Name = "科二地图";
            map.Point0 = "0,0";
            string path = System.AppDomain.CurrentDomain.BaseDirectory+"Place.xml";
            using (FileStream fs = File.Open(path,FileMode.Create,FileAccess.ReadWrite))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(PlaceXmlModel));
                XmlTextWriter textWriter = new XmlTextWriter(fs,Encoding.UTF8);
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("","");
                // 换行
                textWriter.Formatting = Formatting.Indented;
                // 序列化
                xmlSerializer.Serialize(textWriter, map, ns);
                textWriter.Close();
            }

        }

        public void AdjustAxis()
        {
//            FitPlot();
            Chart1.PlotWidth = Chart1.ActualWidth / Chart1.ActualHeight * Chart1.PlotHeight;
        }

        public void RemoveAllPointFromPlot()
        {
            OriginalPoints.Clear();
            int count = Plot1.Children.Count;
            Plot1.Children.RemoveRange(2, count);//剩下坐标TextBlock和MouseNavigation其他全部删除
        }

        private void RemovePointIndex()
        {           
            foreach (var p in OriginalPoints)
            {
                //p.IndexTb.Visibility = Visibility.Collapsed;
                p.IndexTb.Text = "";
            }
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
                FitPlot((MapPoint)e.NewItems[0]);
                DrawMapPoints(Plot1, (MapPoint)e.NewItems[0]);
            }           
        }

        private void FitPlot(MapPoint newPoint)
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
            //List<MapPoint> mps = new List<MapPoint>();

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

        private void DrawMapPoints(PlotBase canva, params MapPoint[] points)
        {
            double pointR = 15;
            int i = 1;
            int count = points.Length;
            foreach (var p in points)
            {
                //MapPoint mp = new MapPoint(p.X, p.Y, pointR);
                canva.Children.Add(p.Shape);

                
                p.IndexTb.Text = (OriginalPoints.Count - count + i).ToString();
                i++;
                Plot.SetX1(p.IndexTb, p.X);
                Plot.SetY1(p.IndexTb, p.Y);
                canva.Children.Add(p.IndexTb);
            }
        }

        public List<MapPoint> reorderTmpPoints;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (orderPointsBtn.Content.ToString() == "调整点序")
            {
                foreach (var p in OriginalPoints)
                {
                    p.Shape.MouseDown -= Point_MouseDown;
                    p.Shape.MouseDown += Point_MouseDown;                  
                }
                RemovePointIndex();
                //selectedCount = 0;
                reorderTmpPoints = new List<MapPoint>();
                orderPointsBtn.Content = "取消";
            }
            else
            {
                orderPointsBtn.Content = "调整点序";
                int i = 1;
                foreach (var mp in OriginalPoints)
                {
                    mp.IndexTb.Text = i.ToString();
                    i++;
                }
            }

        }

        private void P_MouseEnter(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        //int selectedCount;

        private void Point_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var p = sender as Ellipse;
            int selectedMpIndex = OriginalPoints.IndexOf(OriginalPoints.First(mp => mp.Shape == p));
            
            if (reorderTmpPoints.Contains(OriginalPoints[selectedMpIndex]))
            {
                return;
            }
            reorderTmpPoints.Add(OriginalPoints[selectedMpIndex]);
            OriginalPoints[selectedMpIndex].IndexTb.Text = reorderTmpPoints.Count.ToString();
            if (reorderTmpPoints.Count == OriginalPoints.Count)
            {
                MessageBox.Show("点序调整完毕！");
                RemoveAllPointFromPlot();
                foreach (var mp in reorderTmpPoints)
                {
                    OriginalPoints.Add(mp);
                }
                orderPointsBtn.Content = "调整点序";
            }
        }

        private void SwichPoint(ObservableCollection<MapPoint> points,int source,int destination)
        {
            var tmp = points[destination];
            points[destination] = points[source];
            points[source] = tmp;
        }
    }
}
