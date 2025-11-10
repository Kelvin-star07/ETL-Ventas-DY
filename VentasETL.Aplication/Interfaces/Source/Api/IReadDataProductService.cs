namespace VentasETL.Aplication.Interfaces.Source.Api
{
    public interface IReadDataProductService<TentityDto> where TentityDto : class 
    {

        Task<IEnumerable<TentityDto>> GetAllAsync();


    }
}
