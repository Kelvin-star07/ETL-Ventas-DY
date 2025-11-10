using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentasETL.Aplication.Dtos.Source.Api
{
    public class DataProductUpdatedDto
    {

        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ProductNumber { get; set; } = string.Empty;

        public short SafetyStockLevel { get; set; }

        public short ReorderPoint { get; set; }

        public  decimal StandardCost { get; set; }

        public decimal ListPrice { get; set; }

        public int DaysToManufacture { get; set; }

        public DateTime SellStartDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
