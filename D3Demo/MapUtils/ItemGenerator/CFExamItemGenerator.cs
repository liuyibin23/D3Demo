using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using D3Demo.MapUtils.Model;

namespace D3Demo.MapUtils.ItemGenerator
{
    public class CFExamItemGenerator : IExamItemGenerator
    {
        /// <summary>
        /// 4号区和6号区下偏移点
        /// </summary>
        private List<Point> area46BelowOffsetPoints = new List<Point>();
        /// <summary>
        /// 5号区上偏移点
        /// </summary>
        private List<Point> area5AboveOffsetPoints = new List<Point>();

        private double area46OffsetDis = 0.3;//4,6号区偏移距离
        private double area7OffsetDis = 0.3;//7号区偏移距离

        public bool CheckPointCount(int count)
        {
            return count == 10;
        }

        public PlaceXmlModel.Item Generate(IEnumerable<Point> originalPoints)
        {
            if (!CheckPointCount(originalPoints.Count())) return null;
            PlaceXmlModel.Item examItem = new PlaceXmlModel.Item();
            examItem.SubAreas = new PlaceXmlModel.SunArea();
            examItem.SubAreas.Areas = new List<PlaceXmlModel.Area>();

//            double offsetDis = 0.3;//4,6号区偏移距离
            area46BelowOffsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(4), originalPoints.ElementAt(5),
                originalPoints.ElementAt(6), originalPoints.ElementAt(7),originalPoints.ElementAt(8)}, area46OffsetDis);


            examItem.SubAreas.Areas.Add(GenerateArea0(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea1(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea2(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea3(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea4(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea5(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea6(originalPoints));
            examItem.Area = GenerateMainArea(originalPoints);

            examItem.Name = "侧方停车";
            examItem.Flag = "20401";
            examItem.PlaceFlag = "204";
            examItem.Index = "001";
            examItem.Cls = "CF";
            examItem.HaveSensor = "false";
            examItem.StartArea = "2040101";
            examItem.StartMode = "0";

            return examItem;
        }

        /// <summary>
        /// 生成0号区
        /// </summary>
        /// <param name="originalPoints"></param>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea0(IEnumerable<Point> originalPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(0));
            AddPoint2Area(area, originalPoints.ElementAt(1));
            AddPoint2Area(area, originalPoints.ElementAt(8));
            AddPoint2Area(area, originalPoints.ElementAt(9));

            area.Flag = "2040101";
            area.Note = "area0";

            return area;
        }

        /// <summary>
        /// 生成1号区
        /// </summary>
        /// <param name="originalPoints"></param>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea1(IEnumerable<Point> originalPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(1));
            AddPoint2Area(area, originalPoints.ElementAt(2));
            AddPoint2Area(area, originalPoints.ElementAt(5));
            AddPoint2Area(area, originalPoints.ElementAt(8));

            area.Flag = "2040102";
            area.Note = "area1";

            return area;
        }

        /// <summary>
        /// 生成2号区
        /// </summary>
        /// <param name="originalPoints"></param>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea2(IEnumerable<Point> originalPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(2));
            AddPoint2Area(area, originalPoints.ElementAt(3));
            AddPoint2Area(area, originalPoints.ElementAt(4));
            AddPoint2Area(area, originalPoints.ElementAt(5));

            area.Flag = "2040103";
            area.Note = "area2";

            return area;
        }

        /// <summary>
        /// 生成3号区
        /// </summary>
        /// <param name="originalPoints"></param>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea3(IEnumerable<Point> originalPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(8));
            AddPoint2Area(area, originalPoints.ElementAt(5));
            AddPoint2Area(area, originalPoints.ElementAt(6));
            AddPoint2Area(area, originalPoints.ElementAt(7));

            area.Flag = "2040104";
            area.Note = "area3";

            return area;
        }

        /// <summary>
        /// 生成4号区
        /// </summary>
        /// <param name="originalPoints"></param>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea4(IEnumerable<Point> originalPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, area46BelowOffsetPoints[4]);
            AddPoint2Area(area, originalPoints.ElementAt(8));
            AddPoint2Area(area, originalPoints.ElementAt(7));
            AddPoint2Area(area, originalPoints.ElementAt(6));
            AddPoint2Area(area, originalPoints.ElementAt(5));
            AddPoint2Area(area, area46BelowOffsetPoints[1]);
            AddPoint2Area(area, area46BelowOffsetPoints[2]);
            AddPoint2Area(area, area46BelowOffsetPoints[3]);

            area.Flag = "2040105";
            area.Note = "area4";

            return area;
        }

        /// <summary>
        /// 生成5号区
        /// </summary>
        /// <param name="originalPoints"></param>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea5(IEnumerable<Point> originalPoints)
        {
//            double offsetDis = 0.3;//7号区偏移距离
            List<Point> offsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(1), originalPoints.ElementAt(3) }, area7OffsetDis);
            area5AboveOffsetPoints = offsetPoints;
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, offsetPoints[0]);
            AddPoint2Area(area, offsetPoints[1]);
            AddPoint2Area(area, originalPoints.ElementAt(3));
            AddPoint2Area(area, originalPoints.ElementAt(1));

            area.Flag = "2040106";
            area.Note = "area5";

            return area;
        }

        /// <summary>
        /// 生成6号区
        /// </summary>
        /// <param name="originalPoints"></param>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea6(IEnumerable<Point> originalPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(5));
            AddPoint2Area(area, originalPoints.ElementAt(4));
            AddPoint2Area(area, area46BelowOffsetPoints[0]);
            AddPoint2Area(area, area46BelowOffsetPoints[1]);

            area.Flag = "2040107";
            area.Note = "area6";

            return area;
        }

        /// <summary>
        /// 生成主区域
        /// </summary>
        /// <param name="originalPoints"></param>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateMainArea(IEnumerable<Point> originalPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(0));
            AddPoint2Area(area, area5AboveOffsetPoints[0]);
            AddPoint2Area(area, area5AboveOffsetPoints[1]);
            AddPoint2Area(area, area46BelowOffsetPoints[0]);
            AddPoint2Area(area, area46BelowOffsetPoints[2]);
            AddPoint2Area(area, area46BelowOffsetPoints[3]);
            AddPoint2Area(area, originalPoints.ElementAt(9));

            area.Flag = "2040100";
            area.Note = "main";

            return area;
        }

        private void AddPoint2Area(PlaceXmlModel.Area area, Point p)
        {
            p.X = Math.Round(p.X, 3);
            p.Y = Math.Round(p.Y, 3);
            area.Points.Add(new PlaceXmlModel.Point(p.X, p.Y));
        }
    }
}
