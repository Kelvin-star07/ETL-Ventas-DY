

using Microsoft.Extensions.Configuration;
using VentasETL.Aplication.Dtos.Source.Order;
using VentasETL.Domain.Entities.Source;
using VentasETL.Domain.Interfaces.Source;
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
