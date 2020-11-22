using Poc.Encapsulation.Patterns;

namespace Poc.Encapsulation.ExampleLayers
{
    public class BusinessLayer
    {
        private readonly RepositoryLayer repository;

        public BusinessLayer(RepositoryLayer repository)
        {
            this.repository = repository;
        }

        public Result<string> GetBi()
        {
            var result = repository.GetRep();

            if (result.Failure)
            {
                return Output.Error<string>("não foi possível obter resultado do repositório.");
            }

            return result;
        }
    }
}