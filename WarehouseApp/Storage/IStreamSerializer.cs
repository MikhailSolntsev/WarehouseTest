using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Data;
using WarehouseApp.Data.DTO;

namespace WarehouseApp.Storage;

public interface IStreamSerializer
{
    public void Serialize<T>(Stream stream, List<T> value);
    public List<T> Deserialize<T>(Stream stream);
}
