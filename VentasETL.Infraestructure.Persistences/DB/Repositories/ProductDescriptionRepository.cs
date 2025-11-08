
using Microsoft.Extensions.Logging;
using VentasETL.Domain.Entities.DBRead;
using VentasETL.Domain.Interfaces.ReadDb;
using VentasETL.Infraestructure.Persistences.Contexto;

namespace VentasETL.Infraestructure.Persistences.DB.Repositories
{
    public class ProductDescriptionRepository : ReadDataDBRepository<ProductDescription>,IProductDescriptionRepository
    {
        public ProductDescriptionRepository(SourceContext context, ILogger<ProductDescription> logger) : base(context,logger)
        {
        }
    }
}
