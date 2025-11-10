

using Microsoft.Extensions.Configuration;
using VentasETL.Aplication.Dtos.Source.CSV.OrderDetail;
using VentasETL.Aplication.Interfaces.Source.CSV;
using VentasETL.Domain.Entities.Source;
using VentasETL.Domain.Interfaces.Source.CSV;

namespace VentasETL.Aplication.Services.Source
{
    public class OrderDetailService : GenericCSVService<OrderDetail, OrderDetailDto>, IOrderDetailService
    {

        private readonly IOrderDetailRepository repo;
       


        public OrderDetailService(IOrderDetailRepository repo,IConfiguration config) : base(repo, config["CSVPaths:OrderDetail"]!)
        {

            this.repo = repo;
           

        }


    }

}
