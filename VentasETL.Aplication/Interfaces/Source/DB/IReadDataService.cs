namespace VentasETL.Aplication.Interfaces.Source.DB
{
    public interface IReadDataService<TentityDto> where TentityDto : class
    {

        Task<List<TentityDto>> ReadData();

    }
}
