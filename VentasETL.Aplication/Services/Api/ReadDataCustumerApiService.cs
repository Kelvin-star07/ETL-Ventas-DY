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
    public class ReadDataCustumerApiService : ReadDataService<DataCustumerUpdate, DataCustumerUpdatedDto>, IReadDataCustumerApiService<DataCustumerUpdatedDto>
    {

        private const int PageSize = 300;

        public ReadDataCustumerApiService(IApiDataRepository<DataCustumerUpdate> RepoApi) : base(RepoApi)
        {
        }

        public async Task<IEnumerable<DataCustumerUpdatedDto>> GetAllAsync()
        {
            int page = 1;
            var allData = new List<DataCustumerUpdatedDto>();

            while (true)
            {
                string url = $"https://localhost:7015/api/DataCustumer/Get-Custumer?page={page}&pageSize={PageSize}";

                var pageData = await base.GetAllAsync(url);

                if (pageData == null || !pageData.Any())
                    break; 

                allData.AddRange(pageData);
                page++;
            }

            return allData;
        }
    }
}
