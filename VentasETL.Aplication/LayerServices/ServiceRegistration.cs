

using Microsoft.Extensions.DependencyInjection;
using VentasETL.Aplication.Dtos.Api;
using VentasETL.Aplication.Interfaces;
using VentasETL.Aplication.Interfaces.Api;
using VentasETL.Aplication.Interfaces.DB;
using VentasETL.Aplication.Interfaces.Destination;
using VentasETL.Aplication.Services;
using VentasETL.Aplication.Services.Api;
using VentasETL.Aplication.Services.Destination;
using VentasETL.Aplication.Services.ReadDataDb;
using VentasETL.Aplication.Services.Source;
using VentasETL.Domain.Entities.Source;
using VentasETL.Domain.Interfaces.Source;

namespace VentasETL.Aplication.LayerServices
{
    public static  class ServiceRegistration
    {


        public static void AddLayerService(this IServiceCollection service)
        {



            #region configuraciones para los servicios
            service.AddScoped<ICustumerService, CustumerService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IOrderDetailService, OrderDetailService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IHistoricalDataService, HistoricalDataService>();
            service.AddScoped<IProducDescriptionService, ProductDescriptionService>();
            service.AddScoped<ISalesHistoricalDataService, SaleHistoricalDataService>();
            service.AddScoped<IReadDataCustumerApiService<DataCustumerUpdatedDto>, ReadDataCustumerApiService>();
            service.AddScoped<IReadDataProductService<DataProductUpdatedDto>, ReadDataProductApiService>();
            service.AddScoped<ISalesHistoricalDataService, SaleHistoricalDataService>();
            service.AddScoped<ISalesHistoricalDataService, SaleHistoricalDataService>();
            service.AddScoped<IWorkeServiceFinal, WorkeServiceFinal>();
            service.AddScoped<IReadDataCustumerApiService<DataCustumerUpdatedDto>, ReadDataCustumerApiService>();
            service.AddScoped<IReadDataProductService<DataProductUpdatedDto>, ReadDataProductApiService>();
            service.AddScoped(typeof(IGenericDetinationService<>), typeof(GenericDestinationService<>));
            #endregion
        }


    }
}
