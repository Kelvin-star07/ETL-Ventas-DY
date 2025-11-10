

using Microsoft.Extensions.Configuration;
using VentasETL.Aplication.Dtos.Source.CSV.Order;
using VentasETL.Aplication.Interfaces.Source.CSV;
using VentasETL.Domain.Entities.Source;
using VentasETL.Domain.Interfaces.Source.CSV;

namespace VentasETL.Aplication.Services.Source
{
    public class OrderService : GenericCSVService<Order, OrderDto>, IOrderService
    {

        private readonly IOrderRepository repo;
  



        public OrderService(IOrderRepository repo, IConfiguration config) : base(repo, config["CSVPaths:Order"]!)
        {

            this.repo = repo;   
           

        }

    }
}
