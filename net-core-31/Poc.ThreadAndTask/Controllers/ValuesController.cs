using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Poc.ThreadAndTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("v1")]
        public IActionResult Get()
        {
            Console.WriteLine($"[v1]: Started;");
            string xpto = "Thiago";

            new Thread( (value) =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"[v1] Thread: '{xpto}'.");
            })
            .Start(xpto);

            Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine($"[v1] Task[{Task.CurrentId}]: '{xpto}'.");
            });

            return Ok($"Success [v1]");
        }


        [HttpGet("v1/{xpto}")]
        public IActionResult Get(string xpto)
        {
            Console.WriteLine($"[v1/{xpto}]: Started;");

            new Thread((value) =>
            {
                Thread.Sleep(3000);
                Console.WriteLine($"[v1/{xpto}] Thread.");
            })
            .Start(xpto);

            Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine($"[v1/{xpto}] Task[{Task.CurrentId}];");
            });

            return Ok($"Success [v1/{xpto}]");
        }


        [HttpGet("v2")]
        public IActionResult GetV2()
        {
            Console.WriteLine($"[v2]: Started");

            Task.Run(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine($"[v2] Task[{Task.CurrentId}]");
                return $"Origin Task[{Task.CurrentId}] ended.";
            })
            .ContinueWith((output) => 
            {
                output.Wait();
                Console.WriteLine($"[v2] Output: '{output.Result}'.");
                for(int i = 1; i <= 7; i++)
                {
                    Thread.Sleep(10000);
                    Console.WriteLine($"[v2] Task[{Task.CurrentId}]: Tic-Tac {10*i}s;");
                }
            })
            .ContinueWith((output) => 
            {
                if (output.IsCompletedSuccessfully)
                {
                    Console.WriteLine("[v2] Successful");
                }
                else
                {
                    Console.WriteLine("[v2] Failure");
                }
            });

            return Ok("Success [v2]");
        }
    }
}