namespace VentasETL.Aplication.Dtos.Source.CSV.Order
{
    public class OrderDto
    {

        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;

    }
}
