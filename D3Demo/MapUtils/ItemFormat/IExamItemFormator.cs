using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D3Demo.MapUtils.Model;

namespace D3Demo.MapUtils.ItemFormat
{
    public interface IExamItemFormator
    {
        void Format(PlaceXmlModel.Item item);
    }
}
