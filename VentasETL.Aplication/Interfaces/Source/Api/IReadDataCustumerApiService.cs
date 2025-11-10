namespace VentasETL.Aplication.Interfaces.Source.Api
{
    public interface IReadDataCustumerApiService<TentityDto> where TentityDto : class
    {

        Task<IEnumerable<TentityDto>> GetAllAsync();

    }
}
