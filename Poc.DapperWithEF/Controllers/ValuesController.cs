using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Poc.DapperWithEF.Business;

namespace Poc.DapperWithEF.Controllers
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