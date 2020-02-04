using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poc.EFWithManyContexts.Modules.Fruits.Models;
using Poc.EFWithManyContexts.Modules.Occurrences.Models;
using Poc.EFWithManyContexts.Modules.Persons.Models;
using Poc.EFWithManyContexts.Patterns;

namespace Poc.EFWithManyContexts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> logger;
        private readonly UnitOfWorkRepositories repositories;

        public ValuesController(ILogger<ValuesController> logger, UnitOfWorkRepositories repositories)
        {
            this.logger = logger;
            this.repositories = repositories;
        }

        [HttpGet("v1")]
        public IActionResult TestOne()
        {
            try
            {
                var fruit = repositories.Fruits.Add(new FruitModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Maça",
                    Price = 2.1m
                });

                var person = repositories.Persons.Add(new PersonModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Thiago",
                    Login = "godinho",
                    Password = "123"
                });

                var occurrence = repositories.Occurrences.Add(new OccurrenceModel
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    Who = person.Id,
                    What = fruit.Id,
                    Description = "Primeiro teste"
                });

                repositories.Commit();

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Deu ruim");
                throw;
            }
        }

        [HttpGet()]
        public IActionResult TestTwo()
        {
            try
            {
                var fruit = repositories.Fruits.Add(new FruitModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Maça",
                    Price = 2.1m
                });
                repositories.Commit();

                var person = repositories.Persons.Add(new PersonModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Thiago",
                    Login = "godinho",
                    Password = "123"
                });
                repositories.Commit();

                var occurrence = repositories.Occurrences.Add(new OccurrenceModel
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.UtcNow,
                    Who = person.Id,
                    What = fruit.Id,
                    Description = "Primeiro teste"
                });
                repositories.Commit();

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Deu ruim");
                throw;
            }
        }

    }
}
