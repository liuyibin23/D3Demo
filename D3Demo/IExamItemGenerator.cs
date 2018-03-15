using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace D3Demo
{
    public interface IExamItemGenerator
    {
        PlaceXmlModel.Item Generate(IEnumerable<Point> originalPoints);
    }
}
