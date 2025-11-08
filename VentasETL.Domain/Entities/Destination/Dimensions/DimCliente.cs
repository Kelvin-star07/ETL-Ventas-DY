

using System.ComponentModel.DataAnnotations;

namespace VentasETL.Domain.Entities.Destination.Dimensions
{
    public class DimCliente
    {

        [Key]
        public int ClienteKey { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
        public string TipoCliente { get; set; }  = string.Empty ;
        public string Genero { get; set; } = string.Empty;
        public int Edad {  get; set; }
        public string Pais { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Segmento { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }

    }
}
