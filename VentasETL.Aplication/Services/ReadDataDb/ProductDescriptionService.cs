
using VentasETL.Aplication.Dtos.DB;
using VentasETL.Aplication.Interfaces.DB;
using VentasETL.Domain.Entities.DBRead;
using VentasETL.Domain.Interfaces.ReadDb;

namespace VentasETL.Aplication.Services.ReadDataDb
{
    public class ProductDescriptionService : ReadDataService<ProductDescription, ProductDescriptionDto>, IProducDescriptionService
    {
        public ProductDescriptionService(IReadDataDbRepository<ProductDescription> repo) : base(repo)
        {
        }
    }

}
