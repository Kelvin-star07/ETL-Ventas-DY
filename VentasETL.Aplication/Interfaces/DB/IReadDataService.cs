

namespace VentasETL.Aplication.Interfaces.DB
{
    public interface IReadDataService<TentityDto> where TentityDto : class
    {

        Task<List<TentityDto>> ReadData();

    }
}
