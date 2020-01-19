using Newtonsoft.Json;
using System;

namespace Test.StackThrow.Examples
{
    public static class Example02
    {

        /// <summary>
        /// Veja mais detalhes entrando neste método;
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="TgpException">Para 'name' = NULL</exception>
        public static void Execute(string name = null)
        {
            try
            {
                Console.WriteLine($"\nExemplo '{nameof(Example02)}' iniciado!\n\n");
                LastLevel(name: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("A pilha é montada, em formato simplifado, porém não é possível identificar o erro de forma simples!");
                Console.WriteLine("\nFoi gerada uma exceção desconhecida, e agora????\n");

                Console.WriteLine(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
            finally
            {
                Console.WriteLine($"\nExemplo '{nameof(Example02)}' encerrado!\n\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">NULL = TgpException</param>
        /// <exception cref="TgpException">Para 'name' = NULL</exception>
        /// <exception cref="Exception">Para name <> NULL</exception>
        private static void LastLevel(string name = null)
        {
            try
            {
                Console.WriteLine($"[execute]: Inicio do bloco;");
                Level03(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[execute]: Capturar exceção '{ex.GetType().FullName}'!, redefinir o erro e passar a diante.");

                throw;
            }
            finally
            {
                Console.WriteLine("[execute]: Bloco encerrado;");
            }
        }

        private static void Level03(string name = null)
        {
            try
            {
                Console.WriteLine($"[003]: Inicio do bloco;");
                Level02(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[003]: Houve uma exceção '{ex.GetType().FullName}'.");
                throw;
            }
            finally
            {
                Console.WriteLine("[003]: Bloco encerrado.");
            }
        }

        private static void Level02(string name = null)
        {
            try
            {
                Console.WriteLine($"[002]: Inicio do bloco;");
                Level01(name);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[002]: Houve uma exceção '{ex.GetType().FullName}'.");
                throw;
            }
            finally
            {
                Console.WriteLine("[002]: Bloco encerrado.");
            }
        }

        private static void Level01(string name = null)
        {
            try
            {
                Console.WriteLine($"[001]: Inicio do bloco;");
                name.ToLower();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[001]: Houve uma exceção '{ex.GetType().FullName}'.");
                throw;
            }
            finally
            {
                Console.WriteLine("[001]: Bloco encerrado.");
            }
        }
    }
}
