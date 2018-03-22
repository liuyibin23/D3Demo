using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using D3Demo.MapUtils.Model;

namespace D3Demo.MapUtils.ItemGenerator
{
    public class PDExamItemGenerator : IExamItemGenerator
    {
        /// 表示0 - 7号区域的点集
        /// </summary>
        private List<Point>[] areasPoints = new List<Point>[8]; 
        /// <summary>
        /// 主区域点
        /// </summary>
        private List<Point> mainAreaPoints;

        private double area24OffsetDis = 0.5;//2,4号区偏移距离
        private double area5OffsetDis = 0.3;//5号区偏移距离
        private double area0OffsetDis = 2;
        private double area6OffsetDis = 0.3;
        private double area7OffsetDis = 2;

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

            //3号区
            List<Point> offsetPointsTmp =MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(4), originalPoints.ElementAt(3) }, area5OffsetDis);
            areasPoints[3] = new List<Point> { originalPoints.ElementAt(2), offsetPointsTmp[1], offsetPointsTmp[0],originalPoints.ElementAt(1) };

            //2号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { areasPoints[3][2], areasPoints[3][3] }, area24OffsetDis);
            areasPoints[2] = new List<Point> { areasPoints[3][3] , areasPoints[3][2],offsetPointsTmp[0],offsetPointsTmp[1] };

            //4号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { areasPoints[3][0], areasPoints[3][1] }, area24OffsetDis);
            areasPoints[4] = new List<Point> { offsetPointsTmp[0], offsetPointsTmp[1], areasPoints[3][1], areasPoints[3][0] };

            //1号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(5), originalPoints.ElementAt(4) }, area5OffsetDis);
            areasPoints[1] = new List<Point> { areasPoints[2][3], areasPoints[2][2], offsetPointsTmp[0], originalPoints.ElementAt(0) };

            //5号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(2), originalPoints.ElementAt(3) }, area24OffsetDis);
            areasPoints[5] = new List<Point> { areasPoints[4][1], offsetPointsTmp[1], originalPoints.ElementAt(5), areasPoints[1][2], areasPoints[1][1], areasPoints[2][1], areasPoints[3][1] };

            //6号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { areasPoints[5][1], areasPoints[5][2] }, area6OffsetDis);
            areasPoints[6] = new List<Point> { areasPoints[5][1], offsetPointsTmp[0], offsetPointsTmp[1], areasPoints[5][2] };

            //0号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { areasPoints[6][2], originalPoints.ElementAt(0) }, area0OffsetDis);
            areasPoints[0] = new List<Point> { originalPoints.ElementAt(0), areasPoints[6][2], offsetPointsTmp[0], offsetPointsTmp[1] };

            //7号区
            offsetPointsTmp = MathEx.TranslatePoints(new List<Point> { areasPoints[4][0], areasPoints[5][1] }, area7OffsetDis);
            areasPoints[7] = new List<Point> { offsetPointsTmp[0], offsetPointsTmp[1], areasPoints[5][1], areasPoints[4][1], areasPoints[4][0] };

            //主区域
            mainAreaPoints = new List<Point> { areasPoints[7][0], areasPoints[7][1], areasPoints[6][1], areasPoints[6][2], areasPoints[0][2], areasPoints[0][3], areasPoints[0][0], areasPoints[1][0],
            areasPoints[2][0],areasPoints[3][0],areasPoints[4][0]};

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
                area.Flag = "203010" + i;
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
            areaMain.Flag = "2030100";
            areaMain.Note = "main";
            examItem.Area = areaMain;

            examItem.Name = "坡道起步";
            examItem.Flag = "20301";
            examItem.PlaceFlag = "203";
            examItem.Index = "001";
            examItem.Cls = "PD";
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
