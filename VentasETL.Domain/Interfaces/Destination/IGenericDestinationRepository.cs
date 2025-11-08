using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.Destination.Facts;

namespace VentasETL.Domain.Interfaces.Destination
{
    public interface IGenericDestinationRepository<Tentity> where Tentity : class
    {

        Task AddFactVentaAsync(Tentity entity);
        Task AddRangeAsync(IEnumerable<Tentity> entity);
        Task UpdateRangeAsync(IEnumerable<Tentity> entity);
        Task<List<Tentity>> GetAllAsync();
    }
}
