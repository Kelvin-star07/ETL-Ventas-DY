using Microsoft.Extensions.Logging;
using VentasETL.Domain.Entities.Api;
using VentasETL.WebApi.Api.Data.Context;
using VentasETL.WebApi.Api.Data.Interface;

namespace VentasETL.WebApi.Api.Data.Repository
{
    public class ReadDataProductRepository : ReadDataApiRepository<ProductUpdate>, IReadDataProductRepository
    {
        public ReadDataProductRepository(ApiContext context, ILogger<ProductUpdate> logger) : base(context,logger)
        {
        }
    }
}
