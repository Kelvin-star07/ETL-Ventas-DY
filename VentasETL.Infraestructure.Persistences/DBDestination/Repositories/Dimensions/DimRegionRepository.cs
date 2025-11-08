using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.Destination.Dimensions;
using VentasETL.Domain.Interfaces.Destination.Dimensions;
using VentasETL.Infraestructure.Persistences.Contexto;

namespace VentasETL.Infraestructure.Persistences.Destination.Repositories.Dimensions
{
    public class DimRegionRepository : GenericDestinationRepository<DimRegion>, IDimRegionRepository
    {



        public DimRegionRepository(DWHContext context, ILogger<DimRegion> logger) : base(context, logger)
        {






        }


    }

}
