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
    public class DimTiempoRepository : GenericDestinationRepository<DimTiempo>, IDimTiempoRepository
    {

        private readonly DWHContext context;

        public DimTiempoRepository(DWHContext context, ILogger<DimTiempo> logger) : base(context, logger)
        {

            this.context = context;


        }


        public async Task<int> EnsureDateAsync(DateTime fecha)
        {

           
            var existente = await context.Set<DimTiempo>()
                .FirstOrDefaultAsync(f => f.FechaCompleta == fecha);

            if (existente != null)
                return existente.TiempoKey;

          
            var nuevaFecha = new DimTiempo
            {
                FechaCompleta = fecha,
                Anio = fecha.Year,
                Mes = fecha.Month,
                Dia = fecha.Day
            };

            context.Set<DimTiempo>().Add(nuevaFecha);
            await context.SaveChangesAsync();

            return nuevaFecha.TiempoKey;
        }




    }
}
