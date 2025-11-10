namespace VentasETL.Aplication.Dtos.Source.DB
{
    public class HistoricalDataDto
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
