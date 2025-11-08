using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Interfaces.Api;

namespace VentasETL.Infraestructure.Persistences.Api.Repositories
{
    public class ApiDataRepository<T> : IApiDataRepository<T> where T : class
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<T> logger;


        public ApiDataRepository(HttpClient httpClient, ILogger<T> logger)
        {
            _httpClient = httpClient;
            this.logger = logger;
        }


        public async Task<List<T>> GetDataAsync(string endpoint)
        {

            try
            {
                var response = await _httpClient.GetAsync(endpoint);

                response.EnsureSuccessStatusCode();
                logger.LogInformation(response.ToString());

                var data = await response.Content.ReadFromJsonAsync<List<T>>();
                logger.LogInformation("Data exportada correctamente desde la api");
                return data ?? new List<T>();
            }catch(Exception ex)
            {

                logger.LogError("Ocurrio un error al exportar la data desde la api");
                logger.LogError(ex.ToString()); 
                return new List<T>();
            }
        }
    }
}




