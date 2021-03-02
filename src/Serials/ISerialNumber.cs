using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serials
{
    public interface ISerialNumber
    {
        void IncreaseBy(int increase);
        void DecreaseBy(int decrease);
    }
}
