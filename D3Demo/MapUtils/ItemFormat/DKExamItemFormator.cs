using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using D3Demo.MapUtils.Model;

namespace D3Demo.MapUtils.ItemFormat
{
    public class DKExamItemFormator:IExamItemFormator
    {
        PlacePointCompare _pointCompare = new PlacePointCompare();
        public void Format(PlaceXmlModel.Item item)
        {
            AreaRepeatPointDelete(item.Area);
            foreach (var area in item.SubAreas.Areas)
            {
                AreaRepeatPointDelete(area);
            }

            //如果0号区和5号区没有交点，可能是4,5号区位置打反了，交换两个区域位置
            if (!item.SubAreas.Areas[0].Points.Intersect(item.SubAreas.Areas[5].Points, _pointCompare).Any())
            {
                SwichArea(item,4,5);
            }

            IEnumerable<PlaceXmlModel.Point> area01IntersectResult = item.SubAreas.Areas[0].Points.Intersect(item.SubAreas.Areas[1].Points, _pointCompare);
            IEnumerable<PlaceXmlModel.Point> area017intersectResult = area01IntersectResult.Intersect(item.SubAreas.Areas[7].Points, _pointCompare);
            PlaceXmlModel.Point area1FirstPoint = area017intersectResult.ElementAt(0);
            IEnumerable<PlaceXmlModel.Point> area015intersectResult = area01IntersectResult.Intersect(item.SubAreas.Areas[5].Points, _pointCompare);
            PlaceXmlModel.Point area1LastPoint = area015intersectResult.ElementAt(0);

            //调整1号区点序
            ReArrangeAreaPoints(item.SubAreas.Areas[1], FindIndexOf(item.SubAreas.Areas[1], area1FirstPoint),0 , FindIndexOf(item.SubAreas.Areas[1], area1LastPoint), item.SubAreas.Areas[1].Points.Count -1);

            IEnumerable<PlaceXmlModel.Point> area34IntersectResult = item.SubAreas.Areas[3].Points.Intersect(item.SubAreas.Areas[4].Points, _pointCompare);
            IEnumerable<PlaceXmlModel.Point> area35IntersectResult = item.SubAreas.Areas[3].Points.Intersect(item.SubAreas.Areas[5].Points, _pointCompare);
            PlaceXmlModel.Point area3Index0Point = area35IntersectResult.ElementAt(0);
            PlaceXmlModel.Point area3Index1Point = area34IntersectResult.ElementAt(0);            

            ReArrangeAreaPoints(item.SubAreas.Areas[3], FindIndexOf(item.SubAreas.Areas[3], area3Index0Point), 0, FindIndexOf(item.SubAreas.Areas[3], area3Index1Point), 1);
        }

        private int FindIndexOf(PlaceXmlModel.Area area, PlaceXmlModel.Point point)
        {
            return area.Points.FindIndex( p =>  p.X == point.X && p.Y == point.Y );
        }

        /// <summary>
        /// 交换指定Item的两个区域的位置（不交换Flag和Note，只交换区域点）
        /// </summary>
        private void SwichArea(PlaceXmlModel.Item item,int aAreaIndex,int bAreaIndex)
        {
            List<PlaceXmlModel.Point> tmpPoints = item.SubAreas.Areas[aAreaIndex].Points;

            item.SubAreas.Areas[aAreaIndex].Points = item.SubAreas.Areas[bAreaIndex].Points;

            item.SubAreas.Areas[bAreaIndex].Points = tmpPoints;
        }

        private void AreaRepeatPointDelete(PlaceXmlModel.Area area)
        {
            area.Points = area.Points.Distinct(new PlacePointCompare()).ToList();
        }

        private void ReArrangeAreaPoints(PlaceXmlModel.Area area, int firstPointIndex, int lastPointIndex)
        {
            List<PlaceXmlModel.Point> tmpPoints = new List<PlaceXmlModel.Point>();
            if (firstPointIndex - 1 == lastPointIndex)
            {
                //points顺序排列正确
                tmpPoints.AddRange(area.Points.GetRange(firstPointIndex,area.Points.Count - firstPointIndex));
                tmpPoints.AddRange(area.Points.GetRange(0,lastPointIndex + 1));
                area.Points = tmpPoints;
            }
            else if(firstPointIndex + 1 == lastPointIndex)
            {
                //points顺序排列反序
                var fristRange = area.Points.GetRange(0, firstPointIndex + 1);
                fristRange.Reverse();
                tmpPoints.AddRange(fristRange);
                var secondeRange = area.Points.GetRange(lastPointIndex, area.Points.Count - lastPointIndex);
                secondeRange.Reverse();
                tmpPoints.AddRange(secondeRange);
                area.Points = tmpPoints;
            }
            else if(firstPointIndex == 0 && lastPointIndex == area.Points.Count-1)
            {
                //顺序正确和点位置都正确
            }
            else if(firstPointIndex == area.Points.Count - 1 && lastPointIndex == 0)
            {
                area.Points.Reverse();
            }
            else
            {
                throw new ArgumentException("firstPointIndex和lastPointIndex不能覆盖area的所有点");
            }
        }

        private void ReArrangeAreaPoints(PlaceXmlModel.Area area, int aCurrentIndex, int aShouldIndex,
            int bCurrentIndex, int bShouldIndex)
        {
            int count = area.Points.Count;
            int maxCurrentIndex = Math.Max(aCurrentIndex, bCurrentIndex);
            int minCurrentInex = Math.Min(aCurrentIndex,bCurrentIndex);
            if (maxCurrentIndex - minCurrentInex == minCurrentInex + count - maxCurrentIndex)
                throw new ArgumentException("a,b两点在数组中顺时针和逆时针距离相同，需要距离不同的两个点");
            
            //List<PlaceXmlModel.Point> tmpPoints = new List<PlaceXmlModel.Point>();
            PlaceXmlModel.Point[] tmpPoints = new PlaceXmlModel.Point[count];
            if (isOrderCorrect(aCurrentIndex,aShouldIndex,bCurrentIndex,bShouldIndex,count)) //顺序正确
            {
//                tmpPoints[aShouldIndex] = area.Points[aCurrentIndex];
                var indexMap = aShouldIndex - aCurrentIndex;
                for (int i = 0; i < area.Points.Count; i++)
                {
                    int shouldIndex =i + indexMap;
                    if (shouldIndex < area.Points.Count && shouldIndex >= 0)
                    {
                        tmpPoints[shouldIndex] = area.Points[i];
                    }
                    else if(shouldIndex >= area.Points.Count)
                    {
                        shouldIndex = shouldIndex - area.Points.Count;
                        tmpPoints[shouldIndex] = area.Points[i];
                    }
                    else
                    {
                        shouldIndex = shouldIndex + area.Points.Count;
                        tmpPoints[shouldIndex] = area.Points[i];
                    }
                }

            }
            else
            {
                area.Points.Reverse();
                aCurrentIndex = area.Points.Count - 1 - aCurrentIndex;
                bCurrentIndex = area.Points.Count - 1 - bCurrentIndex;

                var indexMap = aShouldIndex - aCurrentIndex;
                for (int i = 0; i < area.Points.Count; i++)
                {
                    int shouldIndex = i + indexMap;
                    if (shouldIndex < area.Points.Count && shouldIndex >= 0)
                    {
                        tmpPoints[shouldIndex] = area.Points[i];
                    }
                    else if (shouldIndex >= area.Points.Count)
                    {
                        shouldIndex = shouldIndex - area.Points.Count;
                        tmpPoints[shouldIndex] = area.Points[i];
                    }
                    else
                    {
                        shouldIndex = shouldIndex + area.Points.Count;
                        tmpPoints[shouldIndex] = area.Points[i];
                    }
                }
            }

            area.Points = tmpPoints.ToList();
        }

        private bool isOrderCorrect(int aCurrentIndex, int aShouldIndex, int bCurrentIndex, int bShouldIndex,int count)
        {
            int difference = bCurrentIndex - aCurrentIndex;
            if(difference >0)
                return aShouldIndex + difference == bShouldIndex || aShouldIndex + difference - count == bShouldIndex;
            else
                return aShouldIndex + difference == bShouldIndex || aShouldIndex + difference + count == bShouldIndex;




//            if (aShouldIndex + difference == bShouldIndex || aShouldIndex + difference - count == bShouldIndex)
//            {
//                
//            }
//            else if(aShouldIndex -difference == bShouldIndex || aShouldIndex -difference + count == bShouldIndex)
//            {
//                
//            }
        }

    }

    public class PlacePointCompare : IEqualityComparer<PlaceXmlModel.Point>
    {
        public bool Equals(PlaceXmlModel.Point a, PlaceXmlModel.Point b)
        {
            if (a != null && b != null)
            {
                return a.X == b.X && a.Y == b.Y;
            }
            return false;
        }

        public int GetHashCode(PlaceXmlModel.Point obj)
        {
            return (obj.X +","+ obj.Y).GetHashCode();
        }
    }
}
