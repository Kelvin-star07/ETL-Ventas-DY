


using System.ComponentModel.DataAnnotations;

namespace VentasETL.Aplication.Dtos.Destination.Dimensiones.DimRegion
{
    public class DimRegionDto
    {


        
        public int RegionKey { get; set; }
        public string Pais { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Zona { get; set; } = string.Empty;


    }
}
