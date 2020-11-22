using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.Encapsulation.ExampleLayers
{
    public class ConnectionLayer
    {
        public string Get()
        {
			try
			{
				return "Resultado de uma consulta";
			}
			catch
			{
				throw;
			}
        }
    }
}
