using System;
using System.Collections.Generic;
using System.Windows;
using D3Demo;
using D3Demo.MapUtils.ItemFormat;
using D3Demo.MapUtils.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var r =MathEx.GetNorthAngle(new Point(0,0), new Point(1,1));
//            Assert.AreEqual(45,r);
        }

        //[TestMethod]
        //public void RepeatPointDeleteTest()
        //{
        //    PlaceXmlModel.Area area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(1,1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    new DKExamItemFormator().AreaRepeatPointDelete(area);
        //}

        //[TestMethod]
        //public void ReArrangeAreaPointsTest()
        //{
        //    PlaceXmlModel.Area area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    new DKExamItemFormator().ReArrangeAreaPoints(area,0,5);

        //    area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));           
        //    new DKExamItemFormator().ReArrangeAreaPoints(area, 3, 2);

        //    area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    new DKExamItemFormator().ReArrangeAreaPoints(area, 2, 3);

        //    area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    new DKExamItemFormator().ReArrangeAreaPoints(area, 5, 0);
        //}

        //[TestMethod]
        //public void IsOrderCorrectTest()
        //{
        //    bool r = new DKExamItemFormator().isOrderCorrect(2,1,5,6,8);
        //    r = new DKExamItemFormator().isOrderCorrect(5, 6, 2, 1, 8);
        //    r = new DKExamItemFormator().isOrderCorrect(1, 5, 5, 1, 8);
        //    r = new DKExamItemFormator().isOrderCorrect( 5, 1, 1, 5, 8);
        //}

        //[TestMethod]
        //public void ReArrangeAreaPointsTest2()
        //{
        //    PlaceXmlModel.Area area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    new DKExamItemFormator().ReArrangeAreaPoints(area, 0, 0,5,5);

        //    area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    new DKExamItemFormator().ReArrangeAreaPoints(area, 3, 0,1,4);

        //    //area = new PlaceXmlModel.Area();
        //    //area.Points = new List<PlaceXmlModel.Point>();
        //    //area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    //new DKExamItemFormator().ReArrangeAreaPoints(area, 2, 0, 5, 3);

        //    area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    new DKExamItemFormator().ReArrangeAreaPoints(area, 2, 0, 3, 5);

        //    area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    new DKExamItemFormator().ReArrangeAreaPoints(area, 2, 0, 4, 4);

        //    //area = new PlaceXmlModel.Area();
        //    //area.Points = new List<PlaceXmlModel.Point>();
        //    //area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    //area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    //new DKExamItemFormator().ReArrangeAreaPoints(area, 2, 0, 5, 3);

        //    area = new PlaceXmlModel.Area();
        //    area.Points = new List<PlaceXmlModel.Point>();
        //    area.Points.Add(new PlaceXmlModel.Point(6, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(5, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(4, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(3, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(2, 1));
        //    area.Points.Add(new PlaceXmlModel.Point(1, 1));
        //    new DKExamItemFormator().ReArrangeAreaPoints(area, 4, 1, 2, 3);
        //    //new DKExamItemFormator().ReArrangeAreaPoints(area, 1, 4, 4, 1);
        //}
    }
}
