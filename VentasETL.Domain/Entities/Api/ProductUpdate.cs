using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasETL.Domain.Entities.Api
{

    [Table("ProductUpdate", Schema = "dbo")]
    public class ProductUpdate
    {



            [Key]
            public int ProductId { get; set; }

            public string Name { get; set; } = string.Empty;

            public string ProductNumber { get; set; } = string.Empty;

            public short SafetyStockLevel { get; set; }

            public short ReorderPoint { get; set; }

            public decimal StandardCost { get; set; }

            public decimal ListPrice { get; set; }

            public int DaysToManufacture { get; set; }

            public DateTime SellStartDate { get; set; }

            public DateTime ModifiedDate { get; set; }
        
    }
}
