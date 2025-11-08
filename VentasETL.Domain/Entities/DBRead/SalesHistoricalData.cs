

using System.ComponentModel.DataAnnotations.Schema;

namespace VentasETL.Domain.Entities.DBRead
{
    [Table("SalesHistoricalData", Schema = "dbo")]
    public class SalesHistoricalData
    {
            public int SalesOrderId { get; set; }

            public int SalesOrderDetailId { get; set; }

            public int ProductId { get; set; }

            public decimal UnitPriceDiscount { get; set; }

            public decimal LineTotal { get; set; }
        
    }
}
