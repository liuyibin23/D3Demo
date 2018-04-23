using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using D3Demo.MapUtils.Model;

namespace D3Demo.MapUtils.ItemGenerator
{
    public class DKExamItemGenerator : IExamItemGenerator
    {
        /// <summary>
        /// 库下面点（3-8号点）偏移出来的点，用于生成4,5,6号区
        /// </summary>
        private List<Point> area456belowOffsetPoints;
        /// <summary>
        /// 0号区左边偏移点
        /// </summary>
        private List<Point> area0LeftOffetPoints;
        /// <summary>
        /// 2号区右边偏移点
        /// </summary>
        private List<Point> area2RightOffetPoints;
        /// <summary>
        /// 7号区上面偏移点
        /// </summary>
        private List<Point> area7AboveOffsetPoints;

        private double area0OffsetDis = 2;//0号区偏移距离
        private double area2OffsetDis = 2;//2号区偏移距离
        private double area456OffsetDis = 0.3;//4,5,6号区偏移距离
        private double area7OffsetDis = 0.3;//7号区偏移距离

        public bool CheckPointCount(int count)
        {
            return count == 8;
        }

        public PlaceXmlModel.Item Generate(IEnumerable<Point> originalPoints)
        {
            if (!CheckPointCount(originalPoints.Count())) return null;
            PlaceXmlModel.Item examItem = new PlaceXmlModel.Item();
            examItem.SubAreas = new PlaceXmlModel.SunArea();
            examItem.SubAreas.Areas = new List<PlaceXmlModel.Area>();

//            double offsetDis = 0.3;//4,5,6号区偏移距离
            area456belowOffsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(2), originalPoints.ElementAt(3),
            originalPoints.ElementAt(4), originalPoints.ElementAt(5),originalPoints.ElementAt(6), originalPoints.ElementAt(7),}, area456OffsetDis);

            examItem.SubAreas.Areas.Add(GenerateArea0(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea1(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea2(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea3(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea4(originalPoints,area456belowOffsetPoints));
            examItem.SubAreas.Areas.Add(GenerateArea5(originalPoints, area456belowOffsetPoints));
            examItem.SubAreas.Areas.Add(GenerateArea6(originalPoints, area456belowOffsetPoints));
            examItem.SubAreas.Areas.Add(GenerateArea7(originalPoints));
            examItem.Area = GenerateMainArea();

            examItem.Name = "倒车入库";
            examItem.Flag = "20101";
            examItem.PlaceFlag = "201";
            examItem.Index = "001";
            examItem.Cls = "DK";
            examItem.HaveSensor = "false";
            examItem.StartArea = "2010103";
            examItem.StartMode = "0";

            return examItem;
        }

        /// <summary>
        /// 生成0号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea0(IEnumerable<Point> originalPoints)
        {
//            double offsetDis = 2;//0号区偏移距离
            List<Point> offsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(7),originalPoints.ElementAt(0)}, area0OffsetDis);
            area0LeftOffetPoints = offsetPoints;
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area,offsetPoints[1]);
            AddPoint2Area(area, originalPoints.ElementAt(0));
            AddPoint2Area(area, originalPoints.ElementAt(7));
            AddPoint2Area(area, offsetPoints[0]);

            area.Flag = "2010101";
            area.Note = "area0";

            return area;
        }

        /// <summary>
        /// 生成1号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea1(IEnumerable<Point> originalPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(0));
            AddPoint2Area(area, originalPoints.ElementAt(1));
            AddPoint2Area(area, originalPoints.ElementAt(2));
            AddPoint2Area(area, originalPoints.ElementAt(7));

            area.Flag = "2010102";
            area.Note = "area1";

            return area;
        }

        /// <summary>
        /// 生成2号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea2(IEnumerable<Point> originalPoints)
        {
//            double offsetDis = 2;//2号区偏移距离
            List<Point> offsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(1), originalPoints.ElementAt(2) }, area2OffsetDis);
            area2RightOffetPoints = offsetPoints;
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();

            AddPoint2Area(area, originalPoints.ElementAt(1));
            AddPoint2Area(area, offsetPoints[0]);
            AddPoint2Area(area, offsetPoints[1]);
            AddPoint2Area(area, originalPoints.ElementAt(2));

            area.Flag = "2010103";
            area.Note = "area2";

            return area;
        }

        /// <summary>
        /// 生成3号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea3(IEnumerable<Point> originalPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(6));
            AddPoint2Area(area, originalPoints.ElementAt(3));
            AddPoint2Area(area, originalPoints.ElementAt(4));
            AddPoint2Area(area, originalPoints.ElementAt(5));

            area.Flag = "2010104";
            area.Note = "area3";

            return area;
        }

        /// <summary>
        /// 生成4号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea4(IEnumerable<Point> originalPoints, List<Point> belowOffsetPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, belowOffsetPoints[4]);
            AddPoint2Area(area, originalPoints.ElementAt(6));
            AddPoint2Area(area, originalPoints.ElementAt(5));
            AddPoint2Area(area, originalPoints.ElementAt(4));
            AddPoint2Area(area, originalPoints.ElementAt(3));
            AddPoint2Area(area, belowOffsetPoints[1]);
            AddPoint2Area(area, belowOffsetPoints[2]);
            AddPoint2Area(area, belowOffsetPoints[3]);

            area.Flag = "2010105";
            area.Note = "area4";

            return area;
        }

        /// <summary>
        /// 生成5号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea5(IEnumerable<Point> originalPoints, List<Point> belowOffsetPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(7));
            AddPoint2Area(area, originalPoints.ElementAt(6));
            AddPoint2Area(area, belowOffsetPoints[4]);
            AddPoint2Area(area, belowOffsetPoints[5]);

            area.Flag = "2010106";
            area.Note = "area5";

            return area;
        }

        /// <summary>
        /// 生成6号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea6(IEnumerable<Point> originalPoints, List<Point> belowOffsetPoints)
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, originalPoints.ElementAt(3));
            AddPoint2Area(area, originalPoints.ElementAt(2));
            AddPoint2Area(area, belowOffsetPoints[0]);
            AddPoint2Area(area, belowOffsetPoints[1]);

            area.Flag = "2010107";
            area.Note = "area6";

            return area;
        }

        /// <summary>
        /// 生成7号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea7(IEnumerable<Point> originalPoints)
        {
//            double offsetDis = 0.3;//7号区偏移距离
            List<Point> offsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(0), originalPoints.ElementAt(1) }, area7OffsetDis);
            area7AboveOffsetPoints = offsetPoints;
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, offsetPoints[0]);
            AddPoint2Area(area, offsetPoints[1]);
            AddPoint2Area(area, originalPoints.ElementAt(1));            
            AddPoint2Area(area, originalPoints.ElementAt(0));

            area.Flag = "2010108";
            area.Note = "area7";

            return area;
        }

        /// <summary>
        /// 生成主区域
        /// </summary>
        /// <param name="originalPoints"></param>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateMainArea()
        {
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, area7AboveOffsetPoints[0]);
            AddPoint2Area(area, area7AboveOffsetPoints[1]);
            AddPoint2Area(area, area2RightOffetPoints[0]);
            AddPoint2Area(area, area2RightOffetPoints[1]);
            AddPoint2Area(area, area456belowOffsetPoints[0]);
            AddPoint2Area(area, area456belowOffsetPoints[1]);
            AddPoint2Area(area, area456belowOffsetPoints[2]);
            AddPoint2Area(area, area456belowOffsetPoints[3]);
            AddPoint2Area(area, area456belowOffsetPoints[4]);
            AddPoint2Area(area, area456belowOffsetPoints[5]);
            AddPoint2Area(area, area0LeftOffetPoints[0]);
            AddPoint2Area(area, area0LeftOffetPoints[1]);

            area.Flag = "2010100";
            area.Note = "main";

            return area;
        }

        private void AddPoint2Area(PlaceXmlModel.Area area,Point p)
        {
            p.X = Math.Round(p.X,3);
            p.Y = Math.Round(p.Y,3);
            area.Points.Add(new PlaceXmlModel.Point(p.X, p.Y));
        }
    }
}
