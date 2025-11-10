
using Microsoft.Extensions.Configuration;
using VentasETL.Aplication.Dtos.Source.CSV.Product;
using VentasETL.Aplication.Interfaces.Source.CSV;
using VentasETL.Domain.Entities.Source;
using VentasETL.Domain.Interfaces.Source.CSV;

namespace VentasETL.Aplication.Services.Source
{ 
    public class ProductService : GenericCSVService<Product, ProductDto>, IProductService   
    {

        private readonly IProductRepository repo;


        public ProductService(IProductRepository repo,IConfiguration config) : base(repo, config["CSVPaths:Product"]!)
        {

            this.repo = repo;   
         

        }

    }
}
