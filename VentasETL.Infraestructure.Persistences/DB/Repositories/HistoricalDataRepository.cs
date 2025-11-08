
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;
using VentasETL.Domain.Entities.DBRead;
using VentasETL.Domain.Interfaces.ReadDb;
using VentasETL.Infraestructure.Persistences.Contexto;

namespace VentasETL.Infraestructure.Persistences.DB.Repositories
{
    public class HistoricalDataRepository : ReadDataDBRepository<HistoricalData>, IHistoricalDataRepository
    {
        public HistoricalDataRepository(SourceContext context, ILogger<HistoricalData> logger) : base(context,logger)
        {
        }
    }
}
