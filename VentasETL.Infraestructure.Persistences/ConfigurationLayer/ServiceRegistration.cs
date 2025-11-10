using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VentasETL.Domain.Entities.Api;
using VentasETL.Domain.Interface.Api;
using VentasETL.Domain.Interfaces.Api;
using VentasETL.Domain.Interfaces.Destination;
using VentasETL.Domain.Interfaces.ReadDb;
using VentasETL.Domain.Interfaces.Source.CSV;
using VentasETL.Infraestructure.Persistences.Api.Repositories;
using VentasETL.Infraestructure.Persistences.Contexto;
using VentasETL.Infraestructure.Persistences.CSV.Repositories;
using VentasETL.Infraestructure.Persistences.DB.Repositories;
using VentasETL.Infraestructure.Persistences.Destination.Repositories;
using VentasETL.WebApi.Api.Data.Context;
using VentasETL.WebApi.Api.Data.Interface;
using VentasETL.WebApi.Api.Data.Repository;

namespace VentasETL.Infraestructure.Persistences.ConfigurationLayer
{
    public static class ServiceRegistration
    {


        public static void AddRegistrationLayerPersistences(this IServiceCollection service,IConfiguration config)
        {



            #region agregando el dbContext

            service.AddDbContext<DWHContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            service.AddDbContext<SourceContext>(opt => opt.UseSqlServer(config.GetConnectionString("Source1Connection")));
            service.AddDbContext<ApiContext>(opt => opt.UseSqlServer(config.GetConnectionString("SourceConnection")));
            #endregion


            #region Configuraciones Para Los Repo
            service.AddScoped<ICustumerRepository, CustumerRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IProductDescriptionRepository, ProductDescriptionRepository>();
            service.AddScoped<IHistoricalDataRepository, HistoricalDataRepository>();
            service.AddScoped<ISalesHistoricalDataRepository, SalesHistoricalDataRepository>();
            service.AddScoped<IReadDataCustumerRepository, ReadDataCustumerUpdateRepository>();
            service.AddScoped<IReadDataProductRepository, ReadDataProductRepository>();
            service.AddScoped(typeof(IGenericDestinationRepository<>), typeof(GenericDestinationRepository<>));
            service.AddScoped(typeof(IApiDataRepository<>), typeof(ApiDataRepository<>));
            service.AddScoped<IApiDataRepository<DataCustumerUpdate>, ApiDataRepository<DataCustumerUpdate>>();
            service.AddHttpClient<IApiDataRepository<ProductUpdate>, ApiDataRepository<ProductUpdate>>();
            service.AddScoped(typeof(IReadDataApiRepository<>), typeof(ReadDataApiRepository<>));
            service.AddScoped(typeof(IReadDataDbRepository<>), typeof(ReadDataDBRepository<>));
         
            #endregion



        }

    }
}
