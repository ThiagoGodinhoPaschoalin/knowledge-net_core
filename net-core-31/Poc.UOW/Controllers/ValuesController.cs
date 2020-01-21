using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Poc.UOW.Business;

namespace Poc.UOW.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ProjectBusiness business;

        public ValuesController(ProjectBusiness business)
        {
            this.business = business;
        }

        [HttpGet]
        public IActionResult AddValues()
        {
            try
            {
                business.Success();
                ///O Success executa o Commit(), mas lembra que no finally, foi colocado o Begin()?
                ///Então, é para situações como estas que seria útil o UOW gerenciar a transação,
                ///Para que você se preocupe com o negócio, e não perder tempo em como gerenciar sua conexão!
                ///NÃO SE ESQUEÇA, isso é somente um exemplo, e não a melhor forma, 
                ///ou até a correta para gerenciar seu projeto...
                business.Failure();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }
    }
}