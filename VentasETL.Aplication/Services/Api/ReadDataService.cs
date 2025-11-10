using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VentasETL.Aplication.Interfaces.Source.Api;
using VentasETL.Domain.Interfaces.Api;

namespace VentasETL.Aplication.Services.Api
{
    public class ReadDataService<Tentity,TentityDto> : IReadDataService<TentityDto> where TentityDto : class , new() where Tentity : class, new()
    {


        private readonly IApiDataRepository<Tentity> RepoApi;




        public ReadDataService(IApiDataRepository<Tentity> RepoApi)
        {
        
           this.RepoApi = RepoApi;
        
        }



        public virtual async Task<IEnumerable<TentityDto>> GetAllAsync(string endpoint)
        {
            var entities = await RepoApi.GetDataAsync(endpoint);
            if (entities == null || !entities.Any())
                return Enumerable.Empty<TentityDto>();

            var cleaned = entities
                .Where(e => e != null)
                .Select(CleanEntity)
                .GroupBy(x => x)
                .Select(g => g.First())
                .ToList();

            return cleaned.Select(MapToDto).ToList();
        }

        private Tentity CleanEntity(Tentity entity)
        {
            foreach (var prop in typeof(Tentity).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var value = prop.GetValue(entity);

                if (prop.PropertyType == typeof(string))
                {
                    string? str = value?.ToString();

                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        str = Regex.Replace(str, @"[^\p{L}\p{N}\s@._-]", "");
                        str = Regex.Replace(str, @"\s{2,}", " ");
                        str = str.Trim();

                        if (prop.Name.ToLower().Contains("name"))
                            str = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
                        else if (prop.Name.ToLower().Contains("email"))
                            str = str.ToLower();
                        else if (prop.Name.ToLower().Contains("category"))
                            str = str.ToUpper();

                        prop.SetValue(entity, str);
                    }
                    else
                    {
                        prop.SetValue(entity, "Desconocido");
                    }
                }
                else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(double))
                {
                    decimal number = Convert.ToDecimal(value ?? 0);
                    if (number < 0) number = 0;
                    prop.SetValue(entity, number);
                }
                else if (prop.PropertyType == typeof(int))
                {
                    int number = Convert.ToInt32(value ?? 0);
                    if (number < 0) number = 0;
                    prop.SetValue(entity, number);
                }
                else if (prop.PropertyType == typeof(DateTime))
                {
                    DateTime fecha;
                    if (value == null || !DateTime.TryParse(value.ToString(), out fecha) || fecha.Year < 1900)
                    {
                        fecha = DateTime.Now;
                    }
                    prop.SetValue(entity, fecha);
                }
            }

            return entity;
        }

        private TentityDto MapToDto(Tentity entity)
        {
            var dto = new TentityDto();

            foreach (var dtoProp in typeof(TentityDto).GetProperties())
            {
                var entityProp = typeof(Tentity).GetProperty(dtoProp.Name,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (entityProp != null)
                {
                    var value = entityProp.GetValue(entity);
                    if (value != null)
                    {
                        try
                        {
                            var converted = Convert.ChangeType(value, dtoProp.PropertyType, CultureInfo.InvariantCulture);
                            dtoProp.SetValue(dto, converted);
                        }
                        catch { }
                    }
                }
            }

            return dto;
        }



    }
}
