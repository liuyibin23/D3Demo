using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace D3Demo
{
    public class ZJExamItemGenerator:IExamItemGenerator
    {
        /// 表示0 - 4号区域的点集
        /// </summary>
        private List<Point>[] areasPoints = new List<Point>[5];
        /// <summary>
        /// 主区域点
        /// </summary>
        private List<Point> mainAreaPoints;

        private double area0OffsetDis = 2;
        private double area2OffsetDis = 2;
        private double area3OffsetDis = 0.3;
        private double area4OffsetDis = 0.3;

        public bool CheckPointCount(int count)
        {
            return count == 6;
        }

        public PlaceXmlModel.Item Generate(IEnumerable<Point> originalPoints)
        {
            if (!CheckPointCount(originalPoints.Count())) return null;
            PlaceXmlModel.Item examItem = new PlaceXmlModel.Item();
            examItem.SubAreas = new PlaceXmlModel.SunArea();
            examItem.SubAreas.Areas = new List<PlaceXmlModel.Area>();

            //0号区
            List<Point> offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(5), originalPoints.ElementAt(0) }, area0OffsetDis);
            areasPoints[0] = new List<Point>{ originalPoints.ElementAt(0), originalPoints.ElementAt(5),offsetPointsTmp[0],offsetPointsTmp[1] };

            //1号区
            areasPoints[1] = new List<Point>();
            foreach (var p in originalPoints)
            {
                areasPoints[1].Add(p);
            }

            //2号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(2), originalPoints.ElementAt(3) }, area2OffsetDis);
            areasPoints[2] = new List<Point>{ offsetPointsTmp [0], offsetPointsTmp [1], originalPoints.ElementAt(3), originalPoints.ElementAt(2) };

            //3号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(0), originalPoints.ElementAt(1), originalPoints.ElementAt(2) }, area3OffsetDis);
            areasPoints[3] = new List<Point>{ originalPoints.ElementAt(0), originalPoints.ElementAt(1), originalPoints.ElementAt(2) ,offsetPointsTmp[2], offsetPointsTmp[1], offsetPointsTmp[0]};

            //4号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(3), originalPoints.ElementAt(4), originalPoints.ElementAt(5) }, area4OffsetDis);
            areasPoints[4] = new List<Point> { originalPoints.ElementAt(3), originalPoints.ElementAt(4), originalPoints.ElementAt(5), offsetPointsTmp[2], offsetPointsTmp[1], offsetPointsTmp[0] };

            mainAreaPoints = new List<Point>
            {
                areasPoints[0][3],areasPoints[3][5],areasPoints[3][4],
                areasPoints[3][3],areasPoints[2][0],areasPoints[2][1],
                areasPoints[4][5],areasPoints[4][4],areasPoints[4][3],
                areasPoints[0][2]
            };

            int i = 1;
            foreach (var points in areasPoints)
            {
                if (points == null) continue;
                var area = new PlaceXmlModel.Area();
                area.Points = new List<PlaceXmlModel.Point>();
                foreach (var p in points)
                {
                    AddPoint2Area(area, p);
                }
                area.Flag = "207010" + i;
                area.Note = "area" + (i - 1);
                examItem.SubAreas.Areas.Add(area);
                i++;
            }

            var areaMain = new PlaceXmlModel.Area();
            areaMain.Points = new List<PlaceXmlModel.Point>();
            foreach (var p in mainAreaPoints)
            {
                AddPoint2Area(areaMain, p);
            }
            areaMain.Flag = "2070100";
            areaMain.Note = "main";
            examItem.Area = areaMain;

            examItem.Name = "直角转弯";
            examItem.Flag = "20701";
            examItem.PlaceFlag = "207";
            examItem.Index = "001";
            examItem.Cls = "ZJ";
            examItem.HaveSensor = "false";
            examItem.StartMode = "0";

            return examItem;
        }

        private void AddPoint2Area(PlaceXmlModel.Area area, Point p)
        {
            p.X = Math.Round(p.X, 3);
            p.Y = Math.Round(p.Y, 3);
            area.Points.Add(new PlaceXmlModel.Point(p.X, p.Y));
        }

    }
}
