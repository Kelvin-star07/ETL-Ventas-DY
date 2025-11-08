

using System.ComponentModel.DataAnnotations.Schema;

namespace VentasETL.Domain.Entities.DBRead
{
    [Table("HistoricalData", Schema ="dbo")]
    public class HistoricalData
    {
            public int TransactionId { get; set; }

            public int ProductId { get; set; }

            public DateTime TransactionDate { get; set; }

            public string TransactionType { get; set; } = string.Empty;

            public int Quantity { get; set; }

            public decimal ActualCost { get; set; }

            public DateTime ModifiedDate { get; set; }
        
    }
}
