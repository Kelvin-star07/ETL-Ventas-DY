

namespace VentasETL.Domain.Interfaces.Source.CSV
{
    public interface IGenericCSVFileRepository<Tentity> : IGenericReadFileRepository<Tentity> where Tentity : class
    {




    }
}
