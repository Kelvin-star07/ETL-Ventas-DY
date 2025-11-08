using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasETL.Domain.Entities.Destination.Dimensions
{
    public class DimProduct
    {

        [Key]
        public int ProductKey { get; set; }
        public string Nombre {  get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string SubCategoria { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; } 
        public decimal PrecioBase { get; set; }
        public string CodigoProducto { get; set; } = string.Empty;
        public string Proveedor { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
         

    }
}
