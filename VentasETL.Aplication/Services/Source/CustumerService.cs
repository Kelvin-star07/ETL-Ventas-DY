using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentasETL.Aplication.Dtos.Source.Custumer;
using VentasETL.Domain.Entities.Source;
using VentasETL.Domain.Interfaces.Source;
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
