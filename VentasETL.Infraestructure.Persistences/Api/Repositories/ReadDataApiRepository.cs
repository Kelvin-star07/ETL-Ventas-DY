using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VentasETL.Domain.Interface.Api;
using VentasETL.WebApi.Api.Data.Context;
using VentasETL.WebApi.Api.Data.Interface;

namespace VentasETL.WebApi.Api.Data.Repository
{
    public class ReadDataApiRepository<Tentity> : IReadDataApiRepository<Tentity> where Tentity : class
    {

        private readonly ApiContext context;
        private readonly ILogger<Tentity> logger;


        public ReadDataApiRepository(ApiContext context, ILogger<Tentity> logger)
        {
        
           this.context = context;  
           this.logger = logger;   
        
        }
        
        
        
        public async Task<List<Tentity>> ReadData()
        {
            try
            {



                var entities =  await context.Set<Tentity>().ToListAsync();
                logger.LogInformation("Data recuperada correctamente desde la api");
                return entities;
            }
            catch (Exception ex) 
            {

                logger.LogError("Ocurrio un error al procesar la data desde la api");
                logger.LogWarning(ex.Message);
                return new List<Tentity>();

            }

        }
    }
}
