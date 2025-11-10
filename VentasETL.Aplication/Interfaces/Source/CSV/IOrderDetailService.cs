using VentasETL.Aplication.Dtos.Source.CSV.OrderDetail;
using VentasETL.Domain.Entities.Source;

namespace VentasETL.Aplication.Interfaces.Source.CSV
{
    public interface IOrderDetailService : IGenericCSVService<OrderDetailDto>
    {

      
    }
}
