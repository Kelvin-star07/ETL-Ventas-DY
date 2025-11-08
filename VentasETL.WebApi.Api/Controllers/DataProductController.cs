using Microsoft.AspNetCore.Mvc;
using VentasETL.WebApi.Api.Data.Interface;

namespace VentasETL.WebApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataProductController : ControllerBase
    {

        private readonly IReadDataProductRepository repo;


        public DataProductController(IReadDataProductRepository repo)
        {
        
        
           this.repo = repo;
        
        }



        [HttpGet("Get-DataProduct")]
        public async Task<IActionResult> GetProduct()
        {

            var listprodut = await  repo.ReadData();
            return Ok(listprodut);

        }
    }
}
