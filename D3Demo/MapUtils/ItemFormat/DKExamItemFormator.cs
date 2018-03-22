using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            IEnumerable<PlaceXmlModel.Point> area01IntersectResult = item.SubAreas.Areas[0].Points.Intersect(item.SubAreas.Areas[1].Points, _pointCompare);
            IEnumerable<PlaceXmlModel.Point> area017intersectResult = area01IntersectResult.Intersect(item.SubAreas.Areas[7].Points, _pointCompare);
            PlaceXmlModel.Point area1FirstPoint = area017intersectResult.ElementAt(0);
            IEnumerable<PlaceXmlModel.Point> area014intersectResult = area01IntersectResult.Intersect(item.SubAreas.Areas[4].Points, _pointCompare);
            PlaceXmlModel.Point area1LastPoint = area014intersectResult.ElementAt(0);
         
            ReArrangeAreaPoints(item.SubAreas.Areas[1], item.SubAreas.Areas[1].Points.IndexOf(area1FirstPoint), item.SubAreas.Areas[1].Points.IndexOf(area1LastPoint));
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
