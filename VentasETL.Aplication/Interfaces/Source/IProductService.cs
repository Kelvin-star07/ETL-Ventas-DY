
using VentasETL.Aplication.Dtos.Source.Product;
using VentasETL.Aplication.Interfaces.Source;
using VentasETL.Domain.Entities.Source;

namespace VentasETL.Domain.Interfaces.Source
{
    public interface IProductService : IGenericCSVService<ProductDto>
    {



    }
}
