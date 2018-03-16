using System;
using System.Windows;
using D3Demo;
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
    }
}
