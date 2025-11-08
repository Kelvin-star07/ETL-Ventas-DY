using VentasETL.Domain.Entities.Source;

namespace VentasETL.Domain.Interfaces.Source.CSV
{
    public interface IOrderRepository : IGenericCSVFileRepository<Order>
    {


    }
}
