

using VentasETL.Aplication.Dtos.DB;
using VentasETL.Aplication.Interfaces.DB;
using VentasETL.Domain.Entities.DBRead;
using VentasETL.Domain.Interfaces.ReadDb;

namespace VentasETL.Aplication.Services.ReadDataDb
{
    public class SaleHistoricalDataService : ReadDataService<SalesHistoricalData, SalesHistoricalDataDto>, ISalesHistoricalDataService
    {
        public SaleHistoricalDataService(IReadDataDbRepository<SalesHistoricalData> repo) : base(repo)
        {
        }
    }
}
