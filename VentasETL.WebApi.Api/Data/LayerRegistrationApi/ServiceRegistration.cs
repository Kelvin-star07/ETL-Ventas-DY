using Microsoft.EntityFrameworkCore;
using VentasETL.Infraestructure.Persistences.Contexto;
using VentasETL.WebApi.Api.Data.Context;
using VentasETL.WebApi.Api.Data.Interface;
using VentasETL.WebApi.Api.Data.Repository;

namespace VentasETL.WebApi.Api.Data.LayerRegistrationApi
{
    public static class ServiceRegistration
    {


        public static void AddLayerApi(this IServiceCollection services, IConfiguration config) 
        {


          


            #region doc
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Ventas ETL API",
                    Version = "v1",
                    Description = "API para procesos ETL de ventas",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Kelvin Díaz Ramírez",
                        Email = "kelvin@example.com"
                    }
                });
            });
            #endregion

            #region agregando el dbContext

            #endregion


        }





    }
}
