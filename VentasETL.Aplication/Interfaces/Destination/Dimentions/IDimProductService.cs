using VentasETL.Aplication.Dtos.Destination.Dimensiones.DimProduct;
using VentasETL.Aplication.Interfaces.Destination;

namespace VentasETL.Aplication.Interfaces.Destination.Dimentions
{
    public interface IDimProductService : IGenericDetinationService<DimProductDto>
    {


        Task<DimProductDto?> GetByKeyAsync(int productKey);


    }


}
