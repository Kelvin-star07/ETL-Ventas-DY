using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Aplication.Dtos.Source.Api;
using VentasETL.Aplication.Interfaces.Source.Api;
using VentasETL.Domain.Entities.Api;
using VentasETL.Domain.Interfaces.Api;

namespace VentasETL.Aplication.Services.Api
{
    public class ReadDataProductApiService : ReadDataService<ProductUpdate, DataProductUpdatedDto>, IReadDataProductService<DataProductUpdatedDto>
    {
        public ReadDataProductApiService(IApiDataRepository<ProductUpdate> RepoApi) : base(RepoApi)
        {
        }

        public async Task<IEnumerable<DataProductUpdatedDto>> GetAllAsync()
        {

            return await base.GetAllAsync("https://localhost:7015/api/DataProduct/Get-DataProduct");

        }

    }
}
