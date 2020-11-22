using Poc.Encapsulation.Patterns;

namespace Poc.Encapsulation.ExampleLayers
{
    public class RepositoryLayer
    {
        private readonly ConnectionLayer connection;

        public RepositoryLayer(ConnectionLayer connection)
        {
            this.connection = connection;
        }

        public Result<string> GetRep()
        {
            return Output.ByRepository(connection.Get);
        }
    }
}
