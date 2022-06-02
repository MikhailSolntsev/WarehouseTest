using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Storage
{
    public interface IStorage
    {
        public void StoreValues<T>(List<T> values);
        public List<T> ReadValues<T>();
    }
}
