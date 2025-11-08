using VentasETL.Aplication.Dtos.Destination.Dimensiones.DimProduct;


namespace VentasETL.Aplication.Interfaces.Destination.Dimentions
{
    public interface IDimProductService : IGenericDetinationService<DimProductDto>
    {


        Task<DimProductDto?> GetByKeyAsync(int productKey);


    }


}
