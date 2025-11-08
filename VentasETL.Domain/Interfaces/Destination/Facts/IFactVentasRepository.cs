using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.Destination.Facts;

namespace VentasETL.Domain.Interfaces.Destination.Facts
{
    public interface IFactVentasRepository : IGenericDestinationRepository<FactVentas>
    {
    }
}
