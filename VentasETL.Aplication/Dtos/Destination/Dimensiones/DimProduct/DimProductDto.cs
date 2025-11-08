

using System.ComponentModel.DataAnnotations;

namespace VentasETL.Aplication.Dtos.Destination.Dimensiones.DimProduct
{
    public class DimProductDto
    {


  
        public int ProductKey { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string SubCategoria { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioBase { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public string CodigoProducto { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

    }
}
