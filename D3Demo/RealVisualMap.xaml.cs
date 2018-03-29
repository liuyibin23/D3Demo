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
using D3Demo.MapUtils.ItemFormat;
using D3Demo.MapUtils.ItemGenerator;
using D3Demo.MapUtils.Model;
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

        private PlaceXmlModel _place;

        public RealVisualMap()
        {
            InitializeComponent();
            OriginalPoints.CollectionChanged += OriginalPoints_CollectionChanged;
            Plot1.MouseMove += Plot1OnMouseMove;
            Chart1.SizeChanged += Chart1_SizeChanged;
            FitPlot();

            //            Plot1.AddHandler(UIElement.MouseDownEvent, (MouseButtonEventHandler)Plot1_OnMouseDown, true);//此种方法可以注册已经被拦截的Plot1的鼠标左键事件
        }

        public void GenerateItem(IExamItemGenerator itemGenerator)
        {
            examItem = itemGenerator.Generate(OriginalPoints.Select(mp=> new Point(mp.X,mp.Y)));
            DrawItem(examItem);
            //foreach (var area in examItem.SubAreas.Areas)
            //{
            //    DrawArea(Plot1,Brushes.Blue, ConvertPointsCollection(area.Points));              
            //}
            //DrawArea(Plot1, Brushes.Blue, ConvertPointsCollection(examItem.Area.Points));
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

        /// <summary>
        /// 保持x轴和y轴的比例正确
        /// </summary>
        public void AdjustAxis()
        {
//            FitPlot();
            Chart1.PlotWidth = Chart1.ActualWidth / Chart1.ActualHeight * Chart1.PlotHeight;
        }

        public void ClearPlot()
        {
            OriginalPoints.Clear();
            int count = Plot1.Children.Count;
            Plot1.Children.RemoveRange(2, count);//剩下坐标TextBlock和MouseNavigation其他全部删除
            selectedAppendedpPolygon = null;
        }

        public void DrawMap(PlaceXmlModel place)
        {
            _place = place;
            foreach (var item in place.Items)
            {
                DrawItem(item);
            }
            //FitMap(place);
            //AdjustAxis();
        }

        private void DrawItem(PlaceXmlModel.Item examItem)
        {
            var polygonMainArea = DrawArea(Plot1, Brushes.Blue, ConvertPointsCollection(examItem.Area.Points));
            DrawText(Plot1,examItem.Flag, double.Parse(examItem.Area.Points[examItem.Area.Points.Count - 1].X), double.Parse(examItem.Area.Points[examItem.Area.Points.Count -1].Y));
            polygonMainArea.Tag = examItem.Area;
            foreach (var area in examItem.SubAreas.Areas)
            {
                var polygonArea = DrawArea(Plot1, Brushes.Blue, ConvertPointsCollection(area.Points));
                polygonArea.Tag = area;
            }           
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

        public void FitMap(PlaceXmlModel place)
        {
            if(place.Items?.Count > 0)
            {
                double _minX = double.MaxValue;
                double _maxX = double.MinValue;
                double _minY = double.MaxValue;
                double _maxY = double.MinValue;
                foreach (var p in place.Items[0].Area?.Points)
                {
                    _maxX = Math.Max(_maxX, double.Parse(p.X));
                    _minX = Math.Min(_minX, double.Parse(p.X));
                    _maxY = Math.Max(_maxY, double.Parse(p.Y));
                    _minY = Math.Min(_minY, double.Parse(p.Y));
                }
                Chart1.PlotOriginX = Math.Round(_minX) - 20;
                Chart1.PlotOriginY = Math.Round(_minY) - 12;
                double height = _maxY - _minY + 36;
                Chart1.PlotHeight = height;
                Chart1.PlotWidth = this.Width / this.Height * height;
            }
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

        private Polygon DrawArea(PlotBase canva, Brush color, params Point[] points)
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
            polygon.Fill = Brushes.Transparent;

            polygon.MouseDown += Polygon_MouseDown;

            Plot.SetPoints(polygon, pc);
            canva.Children.Add(polygon);
            
            //DrawMapPoints(canva, points);
            return polygon;
        }

        Polygon lastSelectedPolygon;
        MapPoint[] lastSelectedMapPoints;
        private TextBlock lastSelectedAreaFlagTb;
        private bool isAreaSelected;//是否有区域被选中
        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ItemAreaSelected(sender as Polygon);
            }
            else if((e.ChangedButton == MouseButton.Right))
            {
                Polygon polygon = sender as Polygon;
                var area = polygon.Tag as PlaceXmlModel.Area;
                var selectedItem = _place.Items.FirstOrDefault(item => item.SubAreas.Areas.Contains(area) || item.Area == area);
                if (selectedItem != null && selectedItem.PlaceFlag == "201")
                {
                    new DKExamItemFormator().Format(selectedItem);
                    ClearPlot();
                    DrawMap(_place);
                }
            }
            
        }

        /// <summary>
        /// 附加选中区域多边形，主要用于描边视觉效果
        /// </summary>
        private Polygon selectedAppendedpPolygon;

        private void ItemAreaSelected(Polygon polygon)
        {
            if (lastSelectedPolygon != null)
            {
                lastSelectedPolygon.Stroke = Brushes.Blue;
                lastSelectedPolygon.StrokeThickness = 2;
            }
            if (lastSelectedAreaFlagTb != null)
            {
                Plot1.Children.Remove(lastSelectedAreaFlagTb);
            }
            RemoveMapPoints(lastSelectedMapPoints);

            //Polygon polygon = sender as Polygon;
            var ps = polygon.Points;
            polygon.Stroke = Brushes.DarkGreen;
            polygon.StrokeThickness = 4;

            if(selectedAppendedpPolygon == null)
            {
                selectedAppendedpPolygon = new Polygon
                {
                    Stroke = Brushes.DarkGreen,
                    StrokeThickness = 4
                };
                
                Plot1.Children.Add(selectedAppendedpPolygon);
            }
            
            

            MapPoint[] mps = new MapPoint[ps.Count];

            int i = 0;
            //foreach(var p in ps)
            //{
            //    double x = Plot1.XFromLeft(p.X);
            //    double y = Plot1.YFromTop(p.Y);
            //    mps[i] = new MapPoint(x, y);
            //    DrawPoint(Plot1,mps[i],i+1);
            //    i++;
            //}
            PointCollection pc = new PointCollection();
            var area = polygon.Tag as PlaceXmlModel.Area;
            foreach (var p in area.Points)
            {
                mps[i] = new MapPoint(double.Parse(p.X), double.Parse(p.Y));
                DrawPoint(Plot1, mps[i], i + 1);
                i++;
                pc.Add(new Point(double.Parse(p.X), double.Parse(p.Y)));
            }
            Plot.SetPoints(selectedAppendedpPolygon, pc);

            lastSelectedAreaFlagTb = DrawText(Plot1, area.Flag, double.Parse(area.Points[0].X), double.Parse(area.Points[0].Y));

            //DrawMapPoints(Plot1, mps);
            lastSelectedPolygon = polygon;
            lastSelectedMapPoints = mps;
            isAreaSelected = true;
        }

        private void Plot1_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isAreaSelected)
            {
                if (lastSelectedPolygon != null)
                {
                    lastSelectedPolygon.Stroke = Brushes.Blue;
                    lastSelectedPolygon.StrokeThickness = 2;
                }
                RemoveMapPoints(lastSelectedMapPoints);
                if (lastSelectedAreaFlagTb != null)
                {
                    Plot1.Children.Remove(lastSelectedAreaFlagTb);
                }

                if (selectedAppendedpPolygon != null)
                {                   
                    Plot1.Children.Remove(selectedAppendedpPolygon);
                    selectedAppendedpPolygon = null;
                }
                isAreaSelected = false;
            }
        }

        private void RemoveMapPoints(MapPoint[] mps)
        {
            if (mps == null) return;
            foreach (var mp in mps)
            {
                if (Plot1.Children.Contains(mp.Shape))
                {
                    Plot1.Children.Remove(mp.Shape);
                }
                if (Plot1.Children.Contains(mp.IndexTb))
                {
                    Plot1.Children.Remove(mp.IndexTb);
                }
            }
        }


        private void DrawPoint(PlotBase canva, MapPoint point,int pointIndex)
        {
            //double pointR = 15;
            canva.Children.Add(point.Shape);
            point.IndexTb.Text = pointIndex.ToString();
            Plot.SetX1(point.IndexTb, point.X);
            Plot.SetY1(point.IndexTb, point.Y);
            canva.Children.Add(point.IndexTb);
        }

        /// <summary>
        /// 画原始点
        /// </summary>
        /// <param name="canva"></param>
        /// <param name="points"></param>
        private void DrawMapPoints(PlotBase canva, params MapPoint[] points)
        {
            //double pointR = 15;
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

        private TextBlock DrawText(PlotBase canva, string text, double x, double y)
        {
            TextBlock tb = new TextBlock();
            tb.Text = text;
            Plot.SetX1(tb, x);
            Plot.SetY1(tb, y);
            canva.Children.Add(tb);
            return tb;
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
                foreach (var p in OriginalPoints)
                {
                    p.Shape.MouseDown -= Point_MouseDown;
                }
            }

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
                ClearPlot();
                foreach (var mp in reorderTmpPoints)
                {
                    OriginalPoints.Add(mp);
                }
                foreach (var op in OriginalPoints)
                {
                    op.Shape.MouseDown -= Point_MouseDown;
                }
                orderPointsBtn.Content = "调整点序";
            }
        }

        
    }
}
