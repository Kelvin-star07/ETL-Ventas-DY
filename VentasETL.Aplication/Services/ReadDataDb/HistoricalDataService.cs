

using VentasETL.Aplication.Dtos.DB;
using VentasETL.Aplication.Interfaces.DB;
using VentasETL.Domain.Entities.DBRead;
using VentasETL.Domain.Interfaces.ReadDb;

namespace VentasETL.Aplication.Services.ReadDataDb
{
    public class HistoricalDataService : ReadDataService<HistoricalData, HistoricalDataDto>, IHistoricalDataService
    {

        public HistoricalDataService(IReadDataDbRepository<HistoricalData> repo) : base(repo)
        {
        }

    }
}
