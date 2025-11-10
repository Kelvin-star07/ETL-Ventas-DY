using VentasETL.Aplication.Dtos.Source.CSV.Order;
using VentasETL.Domain.Entities.Source;

namespace VentasETL.Aplication.Interfaces.Source.CSV
{
    public interface IOrderService : IGenericCSVService<OrderDto>
    {


    }
}
