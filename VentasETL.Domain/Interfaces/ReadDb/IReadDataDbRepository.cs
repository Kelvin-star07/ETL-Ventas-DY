

namespace VentasETL.Domain.Interfaces.ReadDb
{
    public interface IReadDataDbRepository<Tentity> where Tentity : class
    {

        Task<List<Tentity>> ReadData();

    }
}
