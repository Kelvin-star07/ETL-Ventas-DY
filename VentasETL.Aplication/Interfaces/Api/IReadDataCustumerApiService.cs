using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Aplication.Dtos.Api;
using VentasETL.Domain.Entities.Api;

namespace VentasETL.Aplication.Interfaces.Api
{
    public interface IReadDataCustumerApiService<TentityDto> where TentityDto : class
    {

        Task<IEnumerable<TentityDto>> GetAllAsync();

    }
}
