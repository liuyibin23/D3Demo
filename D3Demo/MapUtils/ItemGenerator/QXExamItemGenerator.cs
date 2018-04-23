using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using D3Demo.MapUtils.Model;

namespace D3Demo.MapUtils.ItemGenerator
{
    public class QXExamItemGenerator : IExamItemGenerator
    {
        /// <summary>
        /// 0号区下偏移点
        /// </summary>
        private List<Point> area0BelowffsetPoints;
        /// <summary>
        /// 2号区上偏移点
        /// </summary>
        private List<Point> area2AboveOffsetPoints;
        /// <summary>
        /// 3号区左偏移点
        /// </summary>
        private List<Point> area3LeftOffetPoints;
        /// <summary>
        /// 4号区右偏移点
        /// </summary>
        private List<Point> area4RightOffetPoints;
        /// <summary>
        /// 第一组原始点（左曲线为左边线点，右曲线为右边线点）
        /// </summary>
        private List<Point> firstSideOriginalPoints;
        /// <summary>
        /// 第二组原始点（左曲线为右边线点，右曲线为左边线点）
        /// </summary>
        private List<Point> secondSideOriginalPoints;

        private double area0OffsetDis = 2;
        private double area2OffsetDis = 2;
        private double area3OffsetDis = 0.3;
        private double area4OffsetDis = 0.3;

        public bool CheckPointCount(int count)
        {
            return count > 30 && count % 2 == 0;
        }

        public PlaceXmlModel.Item Generate(IEnumerable<Point> originalPoints)
        {
            if (!CheckPointCount(originalPoints.Count())) return null;

            firstSideOriginalPoints = originalPoints.Take(originalPoints.Count() / 2).ToList();
            secondSideOriginalPoints = originalPoints.Skip(originalPoints.Count() / 2).ToList();

            PlaceXmlModel.Item examItem = new PlaceXmlModel.Item();
            examItem.SubAreas = new PlaceXmlModel.SunArea();
            examItem.SubAreas.Areas = new List<PlaceXmlModel.Area>();

            examItem.SubAreas.Areas.Add(GenerateArea0(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea1(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea2(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea3(originalPoints));
            examItem.SubAreas.Areas.Add(GenerateArea4(originalPoints));
            examItem.Area = GenerateMainArea();

            examItem.Name = "曲线行驶";
            examItem.Flag = "20601";
            examItem.PlaceFlag = "206";
            examItem.Index = "001";
            examItem.Cls = "QX";
            examItem.HaveSensor = "false";
            //examItem.StartArea = "2060103";
            examItem.StartMode = "0";

            return examItem;
        }

        /// <summary>
        /// 生成0号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea0(IEnumerable<Point> originalPoints)
        {
//            double offsetDis = 2;
            List<Point> offsetPoints = MathEx.TranslatePoints(new List<Point> { originalPoints.Last(), originalPoints.ElementAt(0) }, area0OffsetDis);
            area0BelowffsetPoints = offsetPoints;
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();

            AddPoint2Area(area, originalPoints.ElementAt(0));
            AddPoint2Area(area, originalPoints.Last());
            AddPoint2Area(area, offsetPoints[0]);
            AddPoint2Area(area, offsetPoints[1]);

            area.Flag = "2060101";
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

            foreach (var p in originalPoints)
            {
                AddPoint2Area(area,p);
            }

            area.Flag = "2060102";
            area.Note = "area1";

            return area;
        }

        /// <summary>
        /// 生成2号区
        /// </summary>
        /// <returns></returns>
        private PlaceXmlModel.Area GenerateArea2(IEnumerable<Point> originalPoints)
        {
//            double offsetDis = 2;
            List<Point> offsetPoints = MathEx.TranslatePoints(new List<Point> { firstSideOriginalPoints.Last(), secondSideOriginalPoints[0] }, area2OffsetDis);
            area2AboveOffsetPoints = offsetPoints;
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();

            
            AddPoint2Area(area, offsetPoints[0]);
            AddPoint2Area(area, offsetPoints[1]);
            AddPoint2Area(area, secondSideOriginalPoints[0]);
            AddPoint2Area(area, firstSideOriginalPoints.Last());

            area.Flag = "2060103";
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
//            double offsetDis = 0.3;
            List<Point> offsetPoints = MathEx.TranslatePoints(firstSideOriginalPoints, area3OffsetDis);
            area3LeftOffetPoints = offsetPoints;
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();

            foreach (var p in firstSideOriginalPoints)
            {
                AddPoint2Area(area,p);
            }

            for (int i = offsetPoints.Count - 1; i >= 0; i--)
            {
                AddPoint2Area(area, offsetPoints[i]);
            }

            area.Flag = "2060104";
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
//            double offsetDis = 0.3;
            List<Point> offsetPoints = MathEx.TranslatePoints(secondSideOriginalPoints, area4OffsetDis);
            area4RightOffetPoints = offsetPoints;
            PlaceXmlModel.Area area = new PlaceXmlModel.Area();
            area.Points = new List<PlaceXmlModel.Point>();

            foreach (var p in secondSideOriginalPoints)
            {
                AddPoint2Area(area, p);
            }

            for (int i = offsetPoints.Count - 1; i >= 0; i--)
            {
                AddPoint2Area(area, offsetPoints[i]);
            }

            area.Flag = "2060105";
            area.Note = "area4";

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

            AddPoint2Area(area,area0BelowffsetPoints[1]);
            foreach (var p in area3LeftOffetPoints)
            {
                AddPoint2Area(area,p);
            }
            AddPoint2Area(area,area2AboveOffsetPoints[0]);
            AddPoint2Area(area, area2AboveOffsetPoints[1]);
            foreach (var p in area4RightOffetPoints)
            {
                AddPoint2Area(area, p);
            }
            AddPoint2Area(area, area0BelowffsetPoints[0]);

            area.Flag = "2060100";
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
