

using Microsoft.Extensions.Logging;
using VentasETL.Domain.Entities.DBRead;
using VentasETL.Domain.Interfaces.ReadDb;
using VentasETL.Infraestructure.Persistences.Contexto;

namespace VentasETL.Infraestructure.Persistences.DB.Repositories
{
    public class SalesHistoricalDataRepository : ReadDataDBRepository<SalesHistoricalData>, ISalesHistoricalDataRepository
    {
        public SalesHistoricalDataRepository(SourceContext context, ILogger<SalesHistoricalData> logger) : base(context,logger)
        {



        }

    }
}
