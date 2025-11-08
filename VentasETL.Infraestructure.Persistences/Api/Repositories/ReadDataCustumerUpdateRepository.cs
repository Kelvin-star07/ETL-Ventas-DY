using Microsoft.Extensions.Logging;
using VentasETL.Domain.Entities.Api;
using VentasETL.WebApi.Api.Data.Context;
using VentasETL.WebApi.Api.Data.Interface;

namespace VentasETL.WebApi.Api.Data.Repository
{
    public class ReadDataCustumerUpdateRepository : ReadDataApiRepository<DataCustumerUpdate>, IReadDataCustumerRepository
    {
        public ReadDataCustumerUpdateRepository(ApiContext context, ILogger<DataCustumerUpdate> logger)  : base(context,logger)
        {
        }
    }
}
