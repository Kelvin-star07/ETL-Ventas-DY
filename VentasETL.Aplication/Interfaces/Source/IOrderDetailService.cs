

using VentasETL.Aplication.Dtos.Source.OrderDetail;
using VentasETL.Aplication.Interfaces.Source;
using VentasETL.Domain.Entities.Source;

namespace VentasETL.Domain.Interfaces.Source
{
    public interface IOrderDetailService : IGenericCSVService<OrderDetailDto>
    {

      
    }
}
