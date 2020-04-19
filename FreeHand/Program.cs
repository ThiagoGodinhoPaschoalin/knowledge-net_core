using System;
using System.Text.Json;

namespace FreeHand
{
    class Program
    {
        static void Main(string[] args)
        {
            Teste teste = new Teste()
            {
                Name = "Thiago"
            };

            Console.WriteLine( teste.GetType().FullName );
        }
    }

    class Teste
    {
        public string Name { get; set; }
    }
}
