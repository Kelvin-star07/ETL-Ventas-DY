using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.DBRead;

namespace VentasETL.Domain.Interfaces.ReadDb
{
    public interface IHistoricalDataRepository : IReadDataDbRepository<HistoricalData>
    {
    }
}
