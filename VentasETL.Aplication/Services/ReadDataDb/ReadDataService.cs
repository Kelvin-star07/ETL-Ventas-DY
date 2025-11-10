

using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using VentasETL.Aplication.Interfaces.Source.DB;
using VentasETL.Domain.Interfaces.ReadDb;

namespace VentasETL.Aplication.Services.ReadDataDb
{
    public class ReadDataService<Tentity, TentityDto> : IReadDataService<TentityDto> where Tentity : class where TentityDto : class, new()
    {

        private readonly IReadDataDbRepository<Tentity> repo;



        public ReadDataService(IReadDataDbRepository<Tentity> repo)
        {

            this.repo = repo;


        }




        public async Task<List<TentityDto>> ReadData()
        {
            var entities = await repo.ReadData();

            if (entities == null || !entities.Any())
                return new List<TentityDto>();

          
            var cleaned = entities
                .Where(e => e != null)
                .Select(e =>
                {
                    foreach (var prop in typeof(Tentity).GetProperties())
                    {
                        var value = prop.GetValue(e);

                        if (prop.PropertyType == typeof(string))
                        {
                            string? str = value?.ToString();
                            if (!string.IsNullOrWhiteSpace(str))
                            {
                                
                                str = Regex.Replace(str, @"[^\p{L}\p{N}\s@._-]", "");
                                str = Regex.Replace(str, @"\s{2,}", " ").Trim();

                                if (prop.Name.ToLower().Contains("name"))
                                    str = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
                                else if (prop.Name.ToLower().Contains("email"))
                                    str = str.ToLower();
                                else if (prop.Name.ToLower().Contains("category"))
                                    str = str.ToUpper();

                                prop.SetValue(e, str);
                            }
                            else
                            {
                                prop.SetValue(e, "Desconocido");
                            }
                        }
                        else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(double))
                        {
                            decimal number = Convert.ToDecimal(value ?? 0);
                            if (number < 0) number = 0;
                            prop.SetValue(e, number);
                        }
                        else if (prop.PropertyType == typeof(int))
                        {
                            int number = Convert.ToInt32(value ?? 0);
                            if (number < 0) number = 0;
                            prop.SetValue(e, number);
                        }
                        else if (prop.PropertyType == typeof(DateTime))
                        {
                            DateTime fecha;
                            if (value == null || !DateTime.TryParse(value.ToString(), out fecha) || fecha.Year < 1900)
                            {
                                fecha = DateTime.Now;
                            }
                            prop.SetValue(e, fecha);
                        }
                    }

                    return e;
                })
                .GroupBy(x => x) 
                .Select(g => g.First())
                .ToList();

          
            var dtoList = new List<TentityDto>();
            foreach (var entity in cleaned)
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
                            catch
                            {
                                
                            }
                        }
                    }
                }

                dtoList.Add(dto);
            }

            return dtoList;
        }

    }
}
   

