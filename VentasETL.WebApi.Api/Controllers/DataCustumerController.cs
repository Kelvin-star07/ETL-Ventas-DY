using Microsoft.AspNetCore.Mvc;
using VentasETL.WebApi.Api.Data.Interface;

namespace VentasETL.WebApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataCustumerController : ControllerBase
    {

        private readonly IReadDataCustumerRepository repo;



        public DataCustumerController(IReadDataCustumerRepository repo)
        {
        
           this.repo = repo;
        
        }




        [HttpGet("Get-Custumer")]
        public async Task<IActionResult> GetCustumer(int page = 1, int pageSize = 300)
        {
            var custumer = await repo.ReadData();
            var paged = custumer.Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(paged);
        }
    }
}
