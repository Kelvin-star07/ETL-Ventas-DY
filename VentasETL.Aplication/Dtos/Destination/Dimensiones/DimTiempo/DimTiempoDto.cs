

using System.ComponentModel.DataAnnotations;

namespace VentasETL.Aplication.Dtos.Destination.Dimensiones.DimTiempo
{
    public class DimTiempoDto
    {


       
        public int TiempoKey { get; set; }
        public DateTime FechaCompleta { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public string NombreMes { get; set; } = string.Empty;
        public int Semana { get; set; }
        public string DiaSemana { get; set; } = string.Empty;
        public int Trimestre { get; set; }
    }
}
