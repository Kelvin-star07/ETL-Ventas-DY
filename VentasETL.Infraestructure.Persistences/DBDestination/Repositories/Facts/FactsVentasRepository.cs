

using Microsoft.Extensions.Logging;
using VentasETL.Domain.Entities.Destination.Facts;
using VentasETL.Domain.Interfaces.Destination.Facts;
using VentasETL.Infraestructure.Persistences.Contexto;

namespace VentasETL.Infraestructure.Persistences.Destination.Repositories.Facts
{
    public class FactsVentasRepository : GenericDestinationRepository<FactVentas>, IFactVentasRepository
    {
        public FactsVentasRepository(DWHContext context, ILogger<FactVentas> logger) : base(context, logger)
        {


        }
    }
}
