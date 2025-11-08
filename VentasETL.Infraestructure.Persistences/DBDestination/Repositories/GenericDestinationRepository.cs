using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VentasETL.Domain.Interfaces.Destination;
using VentasETL.Infraestructure.Persistences.Contexto;

namespace VentasETL.Infraestructure.Persistences.Destination.Repositories
{
    public class GenericDestinationRepository<Tentity> : IGenericDestinationRepository<Tentity> where Tentity : class
    {


        private readonly DWHContext context;
        private readonly ILogger<Tentity> logger;


        public GenericDestinationRepository(DWHContext context, ILogger<Tentity> logger)
        {
        
           this.context = context;
           this.logger = logger;
        
        }




        public async Task AddFactVentaAsync(Tentity entity)
        {

            try
            {

                if (entity == null)
                {

                    logger.LogWarning("La entidad a guardar esta vacia");

                }

                context.ChangeTracker.Clear();
                await context.Set<Tentity>().AddAsync(entity);
                await context.SaveChangesAsync();
                logger.LogInformation("La entidad a guardar se proceso correctamente");
            }
            catch (Exception ex)
            {


                logger.LogError("Ocurrio un error al guardar la data de una entidad");
                logger.LogError(ex.ToString()); 
                
            }



        }



        public async Task AddRangeAsync(IEnumerable<Tentity> entity)
        {
            try
            {
                if (entity == null)
                {

                    logger.LogWarning("Las entidades a guardar estan vacia");

                }

                context.ChangeTracker.Clear();
                await context.Set<Tentity>().AddRangeAsync(entity);
                await context.SaveChangesAsync();
                logger.LogInformation("La entidad a guardar se proceso correctamente");
            }
            catch (Exception ex)
            {


                logger.LogError("Ocurrio un error al guardar la data de una entidad");
                logger.LogError(ex.ToString());

            }

        }




        public async Task<List<Tentity>> GetAllAsync()
        {

            return await context.Set<Tentity>().ToListAsync();
            

        }



        public async Task UpdateRangeAsync(IEnumerable<Tentity> entities)
        {


            if (entities == null || !entities.Any())
            {

                throw new ArgumentException("La entidades a actualizar estan vacia");

            }


            context.Set<Tentity>().UpdateRange(entities);
            await context.SaveChangesAsync();


        }


    }
}
