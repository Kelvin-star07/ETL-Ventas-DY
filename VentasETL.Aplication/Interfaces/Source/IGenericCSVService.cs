

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VentasETL.Aplication.Interfaces.Source
{
    public interface IGenericCSVService<Tentity> where Tentity : class
    {


        Task<IEnumerable<Tentity>> GetAllAsync();

    }
    
}
