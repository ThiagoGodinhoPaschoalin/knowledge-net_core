using System;
using System.Collections.Generic;

namespace InjectSingletonInView.Singletons
{
    public interface IExampleSingleton
    {
        string GetOne(string key);

        Dictionary<string, string> GetAll();
    }

    public sealed class ExampleSingleton : IExampleSingleton
    {
        private readonly Dictionary<string, string> keyValuePairs; 

        public ExampleSingleton(IServiceProvider provider)
        {
            //provider.GetRequiredService<YourDbContext>().DoAnything();
            keyValuePairs = new Dictionary<string, string>()
            {
                ["first_key"] = "first_value",
                ["second_key"] = "second_value"
            };
        }

        public string GetOne(string key) => keyValuePairs.TryGetValue(key, out string value) ? value : string.Empty;

        public Dictionary<string, string> GetAll() => keyValuePairs;
    }
}
