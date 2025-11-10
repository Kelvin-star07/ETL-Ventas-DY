using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasETL.Aplication.Interfaces.Source.Api
{
    public interface IReadDataService<TentityDto>
    {

        Task<IEnumerable<TentityDto>> GetAllAsync(string enpoint);


    }
}
