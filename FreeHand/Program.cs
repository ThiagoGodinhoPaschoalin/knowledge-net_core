using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FreeHand
{
    public class Error { public string Message { get; set; } }
    public class Success { public int Count { get; set; } }

    public enum RegisterType { Error = 1, Success = 2 }

    public class Register
    {
        public RegisterType Type { get; set; }
        public object Data { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Register> Registers = new Dictionary<int, Register>();

            var error = new Error { Message = "Errou!" };

            Registers.Add(1, new Register { Type = RegisterType.Error, Data = error });

            var reg = Registers.GetValueOrDefault(1);

            swi

            Console.WriteLine( Serialize(Registers) );
            
        }

        static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
    }
}
