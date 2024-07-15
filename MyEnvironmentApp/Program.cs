namespace MyEnvironmentApp
{
    /// <summary>
    /// Program class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            /// Ler uma variável de ambiente específica
            string myVariable = Environment.GetEnvironmentVariable("MINHAVARIAVEL", EnvironmentVariableTarget.Machine);

            if (string.IsNullOrEmpty(myVariable))
            {
                Console.WriteLine("A variável de ambiente 'MINHA_VARIAVEL' não está definida.");
            }
            else
            {
                Console.WriteLine($"Valor de 'MINHA_VARIAVEL': {myVariable}");
            }

            // Listar todas as variáveis de ambiente disponíveis
            Console.WriteLine("\nTodas as variáveis de ambiente:");
            foreach (System.Collections.DictionaryEntry env in Environment.GetEnvironmentVariables())
            {
                Console.WriteLine($"{env.Key} = {env.Value}");
            }
        }
    }
}
