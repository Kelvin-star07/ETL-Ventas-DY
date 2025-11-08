using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Interfaces.ReadDb;
using VentasETL.Infraestructure.Persistences.Contexto;

namespace VentasETL.Infraestructure.Persistences.DB.Repositories
{
    public class ReadDataDBRepository<Tentity> : IReadDataDbRepository<Tentity> where Tentity : class
    {

        private readonly SourceContext context;
        private readonly ILogger<Tentity> logger;

      

        public ReadDataDBRepository(SourceContext context, ILogger<Tentity> logger)
        {
          
             this.context = context;
             this.logger = logger;
        
        
        }

        public async Task<List<Tentity>> ReadData()
        {
            try
            {



                var entities =  await context.Set<Tentity>().ToListAsync();
                logger.LogInformation($"Se recupero correctamente la data desde la db");
                return entities;

            }
            catch (Exception ex)
            {


                logger.LogError($"Error al leer la data desde la db");
                return new List<Tentity>();
            
            }

          
        }
    }
}
