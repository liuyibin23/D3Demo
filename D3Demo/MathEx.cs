using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace D3Demo
{
    public class MathEx
    {
        /// <summary>
        /// 将指定点按指定方向平移指定距离
        /// </summary>
        /// <param name="originPoint">原始点</param>
        /// <param name="angle">指定方向角度(角度)</param>
        /// <param name="dis">指定移动距离</param>
        /// <returns></returns>
        public static Point PointTranslation(Point originPoint, double angle, double dis)
        {
            Point resultPoint = new Point();
            double anglePI = angle * Math.PI / 180;
            resultPoint.X = originPoint.X + dis * Math.Sin(anglePI);
            resultPoint.Y = originPoint.Y + dis * Math.Cos(anglePI);

            return resultPoint;
        }

        /// <summary>
        /// 将给定有序点集向逆/顺时针方向（左/右方向）平移指定距离,平移后的点集组成的折线段和原来的点集组成的折线段平行
        /// </summary>
        /// <param name="points"></param>
        /// <param name="dis"></param>
        /// <param name="isLeft"></param>
        /// <returns></returns>
        public static List<Point> TranslatePoints(List<Point> points, double dis, bool isLeft = true)
        {
            if (points.Count == 1) return null;
            List<Point> translatedPoints = new List<Point>(); //转换后的点集

            List<Point> tempPoints = new List<Point>(); //剔除连续重复点后的点集
            List<int> repetitionIndexList = new List<int>();
            for (int i = 0; i < points.Count; i++)
            {
                if (i > 1 && Math.Abs(points[i].X - points[i - 1].X) < 0.0001 &&
                    Math.Abs(points[i].Y - points[i - 1].Y) < 0.0001) //剔除连续重复点
                {
                    repetitionIndexList.Add(i);
                    continue;
                }
                tempPoints.Add(points[i]);
            }

            for (int i = 0; i < tempPoints.Count; i++)
            {
                double dis2 = 0;
                if (i == 0) //第一个点
                {
                    double angleLast = GetNorthAngle(tempPoints[i], tempPoints[i + 1]); //当前点到下一个点组成的向量的方向角
                    //double verticalAngleLast = AngleSubtraction(angleLast, 90);//向逆时针旋转到与angleLast垂直的角度
                    double verticalAngleLast = isLeft ? angleLast - 90 : angleLast + 90; //向逆时针旋转到与angleLast垂直的角度
                    dis2 = dis;
                    Point p = PointTranslation(tempPoints[i], verticalAngleLast, dis2);
                    translatedPoints.Add(p);
                }
                else if (i != tempPoints.Count - 1) //中间点
                {
                    double a1 =
                        GetAngleOffset(GetNorthAngle(tempPoints[i], tempPoints[i - 1]),
                            GetNorthAngle(tempPoints[i], tempPoints[i + 1])) / 2;
                    dis2 = dis / Math.Sin(Degree2Radian(a1)); //点平移的距离

                    double angleBefore = GetNorthAngle(tempPoints[i - 1], tempPoints[i]); //上一个点与当前点组成的向量的方向角
                    double angleLast = GetNorthAngle(tempPoints[i], tempPoints[i + 1]); //当前点到下一个点组成的向量的方向角
                    //double verticalAngleBefore = AngleSubtraction(angleBefore, 90); //向逆时针旋转到与angleBefore垂直的角度
                    //double verticalAngleLast = AngleSubtraction(angleLast, 90);//向逆时针旋转到与angleLast垂直的角度
                    double verticalAngleBefore = isLeft ? angleBefore - 90 : angleBefore + 90;
                    //向逆时针旋转到与angleBefore垂直的角度
                    double verticalAngleLast = isLeft ? angleLast - 90 : angleLast + 90; //向逆时针旋转到与angleLast垂直的角度

                    Point beforeTempP = PointTranslation(tempPoints[i], verticalAngleBefore, dis);
                    Point lastTempP = PointTranslation(tempPoints[i], verticalAngleLast, dis);

                    Point midTempP = VectorAdd(beforeTempP, lastTempP, tempPoints[i]);
                    double transAngle = GetNorthAngle(new Point(), midTempP); //平移的角度

                    Point p = PointTranslation(tempPoints[i], transAngle, dis2);
                    translatedPoints.Add(p);
                }
                else if (i == tempPoints.Count - 1) //最后一个点
                {
                    double angleBefore = GetNorthAngle(tempPoints[i - 1], tempPoints[i]); //上一个点与当前点组成的向量的方向角
                    //double verticalAngleBefore = AngleSubtraction(angleBefore, 90); //向逆时针旋转到与angleBefore垂直的角度
                    double verticalAngleBefore = isLeft ? angleBefore - 90 : angleBefore + 90;
                    //向逆时针旋转到与angleBefore垂直的角度
                    dis2 = dis;
                    Point p = PointTranslation(tempPoints[i], verticalAngleBefore, dis2);
                    translatedPoints.Add(p);
                }
            }
            //平移后，将连续重复点重新插入点集
            foreach (int t in repetitionIndexList)
            {
                translatedPoints.Insert(t, translatedPoints[t - 1]);
            }
            return translatedPoints;
        }

        /// <summary>
        /// 求两角度间的夹角
        /// </summary>
        /// <param name="angle1"></param>
        /// <param name="angle2"></param>
        /// <returns></returns>
        public static double GetAngleOffset(double angle1, double angle2)
        {
            double num = Math.Abs(angle1 - angle2);
            return num > 180.0 ? 360.0 - num : num;
        }

        /// <summary>
        /// 求线与正北方向的夹角,单位角度
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetNorthAngle(Point p1, Point p2)
        {
            double angle = GetNorthAngleRadian(p1, p2);
            return Radian2Degree(angle);
        }

        /// <summary>
        ///  求线与正北方向的夹角,单位弧度
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double GetNorthAngleRadian(Point p1, Point p2)
        {
            double angle = NorthAngle(p1, p2);
            return angle > 0 ? angle : 2 * Math.PI + angle;
        }

        public static double NorthAngle(Point s, Point e)
        {
            Point o = new Point(0,0);
            Point p2 = new Point(0,10000);
            Point p1 = new Point(e.X - s.X,e.Y - s.Y);
            return Angle(o,p1,p2);
        }

        /// <summary>
        /// 用于求线段之间的夹角
        /// 角度小于PI，返回正值
        /// 角度大于PI，返回负值
        /// </summary>
        /// <param name="o"></param>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns>
        /// 
        /// </returns>
        public static double Angle(Point o, Point s, Point e)
        {
            double cosfi = DotMultiply(s, e, o);

            cosfi /= Dist(o, s) * Dist(o, e);

            if (cosfi >= 1.0) return 0;
            if (cosfi <= -1.0) return -Math.PI;

            double fi = Math.Acos(cosfi);

            if (Multiply(s, e, o) > 0) return fi;

            return -fi;
        }

        /// <summary>
        /// 计算 （p1 -p0）和 (p2-p0) 的点积 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p0"></param>
        /// <returns>
        /// 如果两个矢量都为非零矢量
        /// r 小于 0 两矢量夹角为钝角
        /// r = 0 两矢量夹角为直角
        /// r > 0 两矢量夹角为锐角
        /// </returns>
        public static double DotMultiply(Point p1, Point p2, Point p0)
        {
            return ((p1.X - p0.X) * (p2.X - p0.X) + (p1.Y - p0.Y) * (p2.Y - p0.Y));
        }

        /// <summary>
        /// 计算（sp-op）和（ep-op） 叉积
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="ep"></param>
        /// <param name="op"></param>
        /// <returns>
        /// result > 0  ep在矢量opsp的逆时针方向
        /// result = 0  opspep 三点共线
        /// result 小于 0  ep在矢量opsp的顺时针方向
        /// </returns>
        public static double Multiply(Point sp, Point ep, Point op)
        {
            return ((sp.X - op.X) * (ep.Y - op.Y) - (ep.X - op.X) * (sp.Y - op.Y));
        }

        /// <summary>
        /// 向量（p1 -p0）和 (p2-p0) 的相加的和
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p0"></param>
        /// <returns></returns>
        public static Point VectorAdd(Point p1, Point p2, Point p0)
        {
            Point p3 = new Point();
            p3.X = (p1.X - p0.X) + (p2.X - p0.X);
            p3.Y = (p1.Y - p0.Y) + (p2.Y - p0.Y);
            return p3;
        }

        /// <summary>
        ///  计算两点间的欧氏距离
        /// </summary>
        /// <param name="p1"> 第一个点坐标</param>
        /// <param name="p2">第二个点坐标</param>
        /// <returns>两点间的距离</returns>
        public static double Dist(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        //弧度转角度
        public static double Radian2Degree(double radian)
        {
            return radian * 180 / Math.PI;
        }

        // 角度转弧度
        public static double Degree2Radian(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
}
