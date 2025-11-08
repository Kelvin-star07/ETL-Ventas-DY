using Microsoft.EntityFrameworkCore;
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
    public class DimPorductRepository : GenericDestinationRepository<DimProduct>, IDimProductRepository
    {

        private readonly DWHContext context;


        public DimPorductRepository(DWHContext context,ILogger<DimProduct> logger) : base(context, logger)
        {

            this.context = context;



        }


        public async Task<DimProduct?> GetByKeyAsync(int productKey)
        {

            

            if (productKey < 0)
                throw new ArgumentException($"No se encuentra una  entidad con ese Id: {productKey} ");


            return await context.Set<DimProduct>().FindAsync(productKey);

        }




    }
}

