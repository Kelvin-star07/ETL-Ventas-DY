using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using System.Globalization;
using VentasETL.Domain.Interfaces.Source.CSV;

namespace VentasETL.Infraestructure.Persistences.CSV.Repositories
{
    public class GenericCSVFileRepository<Tentity> : IGenericCSVFileRepository<Tentity> where Tentity : class
    {

        private readonly ILogger<Tentity> logger;


        public GenericCSVFileRepository(ILogger<Tentity> logger)
        {
            this.logger = logger;
        }

        public async Task<IEnumerable<Tentity>> ReadFile(string path)
        {
            try
            {


                if (string.IsNullOrWhiteSpace(path))
                  logger.LogError("El path del archivo CSV no puede ser nulo ni vacío."+(path));

                if (!File.Exists(path))
                  logger.LogWarning($"No se encontró el archivo CSV en la ruta: {path}");

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    IgnoreBlankLines = true,
                    MissingFieldFound = null,
                    HeaderValidated = null,
                    TrimOptions = TrimOptions.Trim,
                    Delimiter = ","
                };

                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, config);

                var records = await Task.Run(() => csv.GetRecords<Tentity>().ToList());
                logger.LogInformation($"El archivo de ruta {path} se proceso correctamente");

                return records;
            }
            catch (Exception ex)
            {


                logger.LogError($"Error al procesar el archivo de ruta {path}");
                throw new Exception();
            
            
            }
        }


    }
}
    



