

using VentasETL.Domain.Entities.Destination.Dimensions;

namespace VentasETL.Domain.Interfaces.Destination.Dimensions
{
    public interface IDimProductRepository : IGenericDestinationRepository<DimProduct>
    {

        Task<DimProduct?> GetByKeyAsync(int productKey);

    }
}
