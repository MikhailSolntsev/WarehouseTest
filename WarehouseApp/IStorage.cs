using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp
{
    public interface IStorage
    {
        public abstract void WriteValues<T>(List<T> value);
        public abstract List<T> ReadValues<T>();
    }
}
