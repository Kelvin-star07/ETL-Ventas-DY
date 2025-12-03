using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.Destination.Facts;

namespace VentasETL.Domain.Interfaces.Destination
{
    public interface IGenericDestinationRepository<T> where T : class
    {

        Task<List<T>> AddRangeReturnAsync(IEnumerable<T> entities);
        Task<T> AddReturnAsync(T entity);

        Task<List<T>> GetAllAsync();
        Task UpdateRangeAsync(IEnumerable<T> entities);

    }
}
