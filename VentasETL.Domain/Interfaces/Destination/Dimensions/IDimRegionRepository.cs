using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.Destination.Dimensions;

namespace VentasETL.Domain.Interfaces.Destination.Dimensions
{
    public interface IDimRegionRepository : IGenericDestinationRepository<DimRegion>
    {
    }
}
