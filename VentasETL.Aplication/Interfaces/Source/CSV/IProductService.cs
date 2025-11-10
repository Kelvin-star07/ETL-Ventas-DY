using VentasETL.Aplication.Dtos.Source.CSV.Product;
using VentasETL.Domain.Entities.Source;

namespace VentasETL.Aplication.Interfaces.Source.CSV
{
    public interface IProductService : IGenericCSVService<ProductDto>
    {



    }
}
