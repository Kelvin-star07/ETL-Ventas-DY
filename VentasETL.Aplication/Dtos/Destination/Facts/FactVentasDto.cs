
using System.ComponentModel.DataAnnotations;

namespace VentasETL.Aplication.Dtos.Destination.Facts
{
    public class FactVentasDto
    {
        public int VentaKey { get; set; }
        public int TiempoId { get; set; }
        public int ProductoId { get; set; }
        public int ClienteId { get; set; }
        public int RegionId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Descuento { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal Costo { get; set; }
        public decimal Margen { get; set; }
        public int NumeroTransaccion { get; set; }

    }

}
