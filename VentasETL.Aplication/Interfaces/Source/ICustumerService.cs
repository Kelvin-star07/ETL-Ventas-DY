

using VentasETL.Aplication.Dtos.Source.Custumer;
using VentasETL.Aplication.Interfaces.Source;
using VentasETL.Domain.Entities.Source;

namespace VentasETL.Domain.Interfaces.Source
{
    public interface ICustumerService : IGenericCSVService<CustumerDto>
    {



    }
}
