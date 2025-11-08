using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasETL.Domain.Entities.Destination.Dimensions
{
    public class DimTiempo
    {

        [Key]
        public int TiempoKey { get; set; }
        public DateTime FechaCompleta { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public string NombreMes { get; set; } = string.Empty;
        public int Semana { get; set; }
        public int Trimestre { get; set; }
        public string DiaSemana { get; set; } = string.Empty;

    }

}
