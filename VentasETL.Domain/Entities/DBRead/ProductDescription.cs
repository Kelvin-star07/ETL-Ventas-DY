

using System.ComponentModel.DataAnnotations.Schema;

namespace VentasETL.Domain.Entities.DBRead
{
    [Table("ProductDescription", Schema = "dbo")]
    public class ProductDescription
    {
        
            public string Description { get; set; } = string.Empty;
        
    }
}
