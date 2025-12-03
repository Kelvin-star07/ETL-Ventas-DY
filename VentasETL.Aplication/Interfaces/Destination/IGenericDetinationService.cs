using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasETL.Aplication.Interfaces.Destination
{
    public interface IGenericDetinationService<Tentity>
    {

        Task AddAsync(Tentity entity);
        Task AddRangeAsync(IEnumerable<Tentity> entity);
      

    }
}
