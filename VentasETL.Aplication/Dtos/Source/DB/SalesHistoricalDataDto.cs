namespace VentasETL.Aplication.Dtos.Source.DB
{
    public class SalesHistoricalDataDto
    {

            public int SalesOrderId { get; set; }
            public int SalesOrderDetailId { get; set; }
            public int ProductId { get; set; }
            public decimal UnitPriceDiscount { get; set; }
            public decimal LineTotal { get; set; }

    }
}
