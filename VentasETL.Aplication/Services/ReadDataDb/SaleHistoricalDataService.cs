using VentasETL.Aplication.Dtos.Source.DB;
using VentasETL.Aplication.Interfaces.Source.DB;
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
