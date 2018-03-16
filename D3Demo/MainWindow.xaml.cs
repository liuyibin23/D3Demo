using System;
using System.Collections;
using System.Collections.Generic;
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
using InteractiveDataDisplay.WPF;
using Microsoft.Win32;

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

            //DrawArea(Figure1, Brushes.Blue, new Point(14184.054, 6744.145), new Point(14186.744, 6757.991), new Point(14193.375, 6756.815), new Point(14190.591, 6742.815));
            //DrawArea(Figure1, Brushes.Blue, new Point(14191.617, 6747.879), new Point(14192.077, 6750.163), new Point(14197.146, 6749.081), new Point(14196.705, 6746.835));
            //Figure1.PlotOriginX = 14180;
            //Figure1.PlotOriginY = 6740;
            //Figure1.PlotHeight = 25;
            //Figure1.PlotWidth = this.Width / this.Height * 25;

            //            Chart2.IsVerticalNavigationEnabled = false;
            //            Chart2.IsHorizontalNavigationEnabled = false;
            //
            //            DrawArea(Plot2, Brushes.Blue, new Point(-7.85, 6.7), new Point(7.85, 6.7), new Point(7.85, 0), new Point(-7.85, 0));
            //            DrawArea(Plot2, Brushes.Blue, new Point(-1.15, 0), new Point(1.15, 0), new Point(1.15, -5.2), new Point(-1.15, -5.2));
            //
            //            Chart2.PlotOriginX = -8;
            //            Chart2.PlotOriginY = -8;
            //            double height2 = 22;
            //            Chart2.PlotHeight = height2;
            //            Chart2.PlotWidth = this.Width / this.Height * height2;
            //            Chart2.SizeChanged += Chart_SizeChanged;

            //TextBlock text = new TextBlock();
            //text.Text = "1";
            //text.FontSize = 20;
            //Plot.SetX1(text,-7.85);
            //Plot.SetY1(text, 6.7);
            //Plot2.Children.Add(text);

            ////0
            //DrawArea(Plot1,Brushes.Red, new Point(14190.491, 6742.325), new Point(14183.954, 6743.655), new Point(14184.054, 6744.145), new Point(14190.591, 6742.815));
            ////1
            //DrawArea(Plot1,Brushes.Blue, new Point(14184.054, 6744.145), new Point(14186.744, 6757.991), new Point(14193.375, 6756.815), new Point(14190.591, 6742.815));
            ////2
            //DrawArea(Plot1,Brushes.Red, new Point(14186.744, 6757.991), new Point(14186.866, 6758.680), new Point(14193.497, 6757.504), new Point(14193.375, 6756.815));
            ////3
            //DrawArea(Plot1,Brushes.Blue, new Point(14191.617, 6747.879), new Point(14192.077, 6750.163), new Point(14197.146, 6749.081), new Point(14196.705, 6746.835));

            //            DrawMapPoints(Plot1, new Point(14184.054, 6744.145), new Point(14186.744, 6757.991), new Point(14193.375, 6756.815), new Point(14190.591, 6742.815));
            //            DrawMapPoints(Plot1,new Point(14191.617, 6747.879), new Point(14192.077, 6750.163), new Point(14197.146, 6749.081), new Point(14196.705, 6746.835));
            //
            //            Chart1.PlotOriginX = 14180;
            //            Chart1.PlotOriginY = 6740;
            //            double height = 25;
            //            Chart1.PlotHeight = height;
            //            Chart1.PlotWidth = this.Width / this.Height * height;
            //            Chart1.SizeChanged += Chart_SizeChanged;

            //Plot1.Children.OfType<MouseNavigation>().ToList()[0]
            //mouse.MouseMove += Mouse_MouseMove;


            //RealVisualMap1.OriginalPoints.Add(new Point(14184.054, 6744.145));
            //RealVisualMap1.OriginalPoints.Add(new Point(14186.744, 6757.991));
            //RealVisualMap1.OriginalPoints.Add(new Point(14193.375, 6756.815));
            //RealVisualMap1.OriginalPoints.Add(new Point(14192.077, 6750.163));
            //RealVisualMap1.OriginalPoints.Add(new Point(14197.146, 6749.081));
            //RealVisualMap1.OriginalPoints.Add(new Point(14196.705, 6746.835));
            //RealVisualMap1.OriginalPoints.Add(new Point(14191.617, 6747.879));
            //RealVisualMap1.OriginalPoints.Add(new Point(14190.591, 6742.815));

            //
            //            RealVisualMap1.GenerateItem(new DKExamItemGenerator());



            //RealVisualMap1.OriginalPoints.Add(new Point(14184.054, 6744.145));
            //RealVisualMap1.OriginalPoints.Add(new Point(14186.744, 6757.991));
            //RealVisualMap1.OriginalPoints.Add(new Point(14193.375, 6756.815));
            //RealVisualMap1.OriginalPoints.Add(new Point(14190.591, 6742.815));
            //RealVisualMap1.OriginalPoints.Add(new Point(14191.617, 6747.879));
            //RealVisualMap1.OriginalPoints.Add(new Point(14192.077, 6750.163));
            //RealVisualMap1.OriginalPoints.Add(new Point(14197.146, 6749.081));
            //RealVisualMap1.OriginalPoints.Add(new Point(14196.705, 6746.835));
        }

        private void Chart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var chart = sender as Chart;
            chart.PlotWidth = e.NewSize.Width / e.NewSize.Height * chart.PlotHeight;
        }

        private void DrawArea(PlotBase canva, Brush color,params Point[] points)
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
            canva.Children.Add(polygon);
            DrawMapPoints(canva,points);
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

//        private void DrawMapPoints(PlotBase canva,IEnumerable<Point> points)
//        {
//            double pointR = 15;
//            foreach (var p in points)
//            {
//                MapPoint mp = new MapPoint(p.X, p.Y, pointR);
//                canva.Children.Add(mp.Shape);
//            }
//        }

        

        private void Chart1_Loaded(object sender, RoutedEventArgs e)
        {
            //double height = 20;
            //Chart1.PlotHeight = height;
            //Chart1.PlotWidth = Chart1.Width / Chart1.Height * height;
            //Chart1.SizeChanged += Chart1_SizeChanged;
        }

        private void Plot1_MouseMove(object sender, MouseEventArgs e)
        {
            //var p = e.GetPosition(sender as IInputElement);
            //var x = Plot1.XFromLeft(p.X);
            //var y = Plot1.YFromTop(p.Y);
            //coordinateTB.Text = $"{x},{y}";
            //Console.WriteLine($"{x},{y}");
        }

        //private void MouseNavigation_MouseMove(object sender, MouseEventArgs e)
        //{

        //}
        private string _pointPath;
        private void Btn_LoadPoint_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            var r = ofd.ShowDialog();
            if (r != null && r == true)
            {
                _pointPath = ofd.FileName;
                LoadPoint(_pointPath);
            }
        }

        private List<Point> originPoints = new List<Point>();

        private void LoadPoint(string path)
        {
            if (File.Exists(path))
            {
                originPoints.Clear();
                RealVisualMap1.OriginalPoints.Clear();
                //                var es = RealVisualMap1.Plot1.Children.OfType<Ellipse>();
                //
                //                RealVisualMap1.Plot1.Children.RemoveRange(0,1);
                //                foreach (var ellipse in es)
                //                {
                //                    RealVisualMap1.Plot1.Children.Remove(ellipse);
                //                }
                int count = RealVisualMap1.Plot1.Children.Count;
                //for (int i = 0 ;i < count; i++)
                //{
                //    if (RealVisualMap1.Plot1.Children[i] is Ellipse)
                //    {
                //        RealVisualMap1.Plot1.Children.Remove(i);
                //    }
                //}
                RealVisualMap1.Plot1.Children.RemoveRange(1, count);
                    using (StreamReader sr = new StreamReader(path))
                {
                    string str;
                    while ((str = sr.ReadLine()) != null)
                    {
                        string coordinateStr = str.Split(' ')[2];
                        string[] coordinateStrs = coordinateStr.Split(',');
                        string x = coordinateStrs[0].Split('=')[1];
                        string y = coordinateStrs[1].Split('=')[1];
                        y = y.Substring(0, y.Length - 1);
                        originPoints.Add(new Point(double.Parse(x),double.Parse(y)));
                        RealVisualMap1.OriginalPoints.Add(originPoints.Last());
                    }                    
                }
                RealVisualMap1.AdjustAxis();
            }
        }

        private void Btn_GeneratDK_OnClick(object sender, RoutedEventArgs e)
        {
            if (RealVisualMap1.OriginalPoints.Count != 8)
            {
                MessageBox.Show("原始点数量不对！");
                return;
            }
            RealVisualMap1.GenerateItem(new DKExamItemGenerator());
            RealVisualMap1.ExportMap();
            MessageBox.Show("生成的地图文件在程序根目录下，目前还需要手动修改项目Flag,区域Flag,和开始区域！");
        }
    }
}
