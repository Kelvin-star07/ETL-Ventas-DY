namespace VentasETL.Domain.Interfaces.Api
{
    public interface IApiDataRepository<T> where T : class
    {
        Task<List<T>> GetDataAsync(string endpoint);
    }
}