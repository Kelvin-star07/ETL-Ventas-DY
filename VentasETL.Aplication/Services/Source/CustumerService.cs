using Microsoft.Extensions.Configuration;
using VentasETL.Aplication.Dtos.Source.CSV.Custumer;
using VentasETL.Aplication.Interfaces.Source.CSV;
using VentasETL.Domain.Entities.Source;
using VentasETL.Domain.Interfaces.Source.CSV;

namespace VentasETL.Aplication.Services.Source
{
    public class CustumerService : GenericCSVService<Custumer, CustumerDto>, ICustumerService
    {

        private readonly ICustumerRepository repo;
     

       
        public CustumerService(ICustumerRepository repo, IConfiguration config) : base(repo,config["CSVPaths:Customer"]!)
        {

            this.repo = repo;
           
          
        }

    }

}
