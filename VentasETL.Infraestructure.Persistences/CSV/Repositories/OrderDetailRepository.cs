using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Domain.Entities.Source;
using VentasETL.Domain.Interfaces.Source.CSV;

namespace VentasETL.Infraestructure.Persistences.CSV.Repositories
{
    public class OrderDetailRepository : GenericCSVFileRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ILogger<OrderDetail> logger) : base(logger)
        {
        }
    }
}
