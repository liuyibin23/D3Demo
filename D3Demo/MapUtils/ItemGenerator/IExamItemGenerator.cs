using System.Collections.Generic;
using System.Windows;
using D3Demo.MapUtils.Model;

namespace D3Demo.MapUtils.ItemGenerator
{
    public interface IExamItemGenerator
    {
        PlaceXmlModel.Item Generate(IEnumerable<Point> originalPoints);
        /// <summary>
        /// 检查所需原始点数量是否正确
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        bool CheckPointCount(int count);
    }
}
