using System;
using System.Text.Json;

namespace ConsoleReflection
{
    public class InvokeMethodTraining
    {
        public InvokeMethodTraining()
        { }

        public static void Execute(int input)
        {
            try
            {
                var result = typeof(InvokeMethodTraining)
                .GetMethod(nameof(NumberToString))
                .Invoke(new InvokeMethodTraining(), System.Reflection.BindingFlags.Public, null, new object[] { input }, null);

                Console.WriteLine($"Input  :: Type[ {input.GetType().Name} ] -> {input}");

                var type = result.GetType().Name;
                var value = JsonSerializer.Serialize(result, result.GetType(), new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine($"Output :: Type[ {type} ] -> {value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public string NumberToString(int number) => number.ToString();
    }
}