

using System.ComponentModel.DataAnnotations;

namespace VentasETL.Domain.Entities.Destination.Dimensions
{
    public class DimRegion
    {

        [Key]
        public int RegionKey { get; set; }
        public string Pais { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty ;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Zona { get; set; } = string.Empty;    

    }
}
