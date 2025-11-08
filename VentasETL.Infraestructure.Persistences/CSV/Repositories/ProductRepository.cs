

using Microsoft.Extensions.Logging;
using VentasETL.Domain.Entities.Source;
using VentasETL.Domain.Interfaces.Source.CSV;

namespace VentasETL.Infraestructure.Persistences.CSV.Repositories
{
    public class ProductRepository : GenericCSVFileRepository<Product>, IProductRepository
    {
        public ProductRepository(ILogger<Product> logger) : base(logger)
        {
        }
    }
}
