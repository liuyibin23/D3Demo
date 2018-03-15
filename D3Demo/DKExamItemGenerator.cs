using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace D3Demo
{
    public class DKExamItemGenerator : IExamItemGenerator
    {
        /// <summary>
        /// 库下面点（3-8号点）偏移出来的点，用于生成4,5,6号区
        /// </summary>
        List<Point> belowOffsetPoints;
        public PlaceXmlModel.Item Generate(IEnumerable<Point> originalPoints)
        {
            if (originalPoints.Count() != 8) return null;
            PlaceXmlModel.Item examItem = new PlaceXmlModel.Item();
            examItem.SubAreas = new PlaceXmlModel.SunArea();
            examItem.SubAreas.Areas = new List<PlaceXmlModel.Area>();

            double offsetDis = 0.3;
            belowOffsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(2), originalPoints.ElementAt(3),
            originalPoints.ElementAt(4), originalPoints.ElementAt(5),originalPoints.ElementAt(6), originalPoints.ElementAt(7),}, offsetDis);

            examItem.SubAreas.Areas.Add(GenerateArea0(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea1(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea2(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea3(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea4(originalPoints,belowOffsetPoints));
            examItem.SubAreas.Areas.Add(GenerateArea5(originalPoints, belowOffsetPoints));
            examItem.SubAreas.Areas.Add(GenerateArea6(originalPoints, belowOffsetPoints));
            examItem.SubAreas.Areas.Add(GenerateArea7(originalPoints));

            return examItem;
        }

        /// <summary>
        /// 生成0号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea0(IEnumerable<Point> originalPoints)
        {
            double offsetDis = 2;
            List<Point> offsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(7),originalPoints.ElementAt(0)}, offsetDis);
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area,offsetPoints[1]);
            AddPoint2Area(area, originalPoints.ElementAt(0));
            AddPoint2Area(area, originalPoints.ElementAt(7));
            AddPoint2Area(area, offsetPoints[0]);
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
            return area;
        }

        /// <summary>
        /// 生成2号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea2(IEnumerable<Point> originalPoints)
        {
            double offsetDis = 2;
            List<Point> offsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(1), originalPoints.ElementAt(2) }, offsetDis);
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();

            AddPoint2Area(area, originalPoints.ElementAt(1));
            AddPoint2Area(area, offsetPoints[0]);
            AddPoint2Area(area, offsetPoints[1]);
            AddPoint2Area(area, originalPoints.ElementAt(2));

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
            return area;
        }

        /// <summary>
        /// 生成7号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea7(IEnumerable<Point> originalPoints)
        {
            double offsetDis = 0.3;
            List<Point> offsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.ElementAt(0), originalPoints.ElementAt(1) }, offsetDis);
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();
            AddPoint2Area(area, offsetPoints[0]);
            AddPoint2Area(area, offsetPoints[1]);
            AddPoint2Area(area, originalPoints.ElementAt(1));            
            AddPoint2Area(area, originalPoints.ElementAt(0));

            return area;
        }

        ///// <summary>
        ///// 生成主区域
        ///// </summary>
        ///// <param name="originalPoints"></param>
        ///// <returns></returns>
        //private PlaceXmlModel.Area GenerateMainArea(IEnumerable<Point> originalPoints)
        //{
        //    PlaceXmlModel.Area area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();

        //}

        private void AddPoint2Area(PlaceXmlModel.Area area,Point p)
        {
            area.Points.Add(new PlaceXmlModel.Point(p.X, p.Y));
        }
    }
}
