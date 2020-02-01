using System.Collections.Generic;

namespace Poc.ThreadAndTask.Repositories
{
    public class LocalRepository : ILocalRepository
    {
        private readonly Dictionary<string, string> data;

        public LocalRepository()
        {
            data = new Dictionary<string, string>
            {
                ["Key"] = "xpto123",
                ["Secret"] = "xptoABC"
            };
        }

        public object GetAllData() => data;
    }

    public interface ILocalRepository
    {
        object GetAllData();
    }
}