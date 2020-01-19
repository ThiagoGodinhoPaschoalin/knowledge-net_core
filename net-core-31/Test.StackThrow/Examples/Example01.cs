using Newtonsoft.Json;
using System;

namespace Test.StackThrow.Examples
{
    public static class Example01
    {
        public static void Execute()
        {
            try
            {
                Console.WriteLine($"\nExemplo '{nameof(Example01)}' iniciado!\n\n");
                LastLevel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\nO que acontecerá aqui?\n" +
                    $"A cada interação com o catch, eu fui substituindo o Stack da exceção pelo atual.\n" +
                    $"Isso manteve o 'ClassName' e a 'Message' da exceção de origem, mas só sei qual foi o último método que empilhou a exceção.");

                Console.WriteLine("\n\nNUNCA FAÇA ISSO!\n\n");

                Console.WriteLine(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
            finally
            {
                Console.WriteLine($"\nExemplo '{nameof(Example01)}' encerrado!\n\n");
            }
        }

        private static void LastLevel()
        {
            try
            {
                Level03();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void Level03()
        {
            try
            {
                Level02();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void Level02()
        {
            try
            {
                Level01();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static void Level01()
        {
            try
            {
                Console.WriteLine("[01]: Tentar Converter cadeia de caracteres para minúsculo.");
                string name = null;
                name.ToLower();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}