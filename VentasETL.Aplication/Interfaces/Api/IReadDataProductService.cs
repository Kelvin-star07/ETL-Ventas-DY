

using VentasETL.Aplication.Dtos.Api;

namespace VentasETL.Aplication.Interfaces.Api
{
    public interface IReadDataProductService<TentityDto> where TentityDto : class 
    {

        Task<IEnumerable<TentityDto>> GetAllAsync();


    }
}
