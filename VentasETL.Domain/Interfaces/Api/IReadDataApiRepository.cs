namespace VentasETL.Domain.Interface.Api
{
    public interface IReadDataApiRepository<Tentity>
    {

        Task<List<Tentity>> ReadData();



    }
}
