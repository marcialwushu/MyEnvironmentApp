using System.Diagnostics;

namespace SetEnvApp
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
            // Exemplo de uso: salvar a variável de ambiente "TEST_VAR" com o valor "Hello World"
            SaveEnvironmentVariable("MINHAVARIAVEL", "Hello World", forAllUsers: true);
        }

        /// <summary>
        /// Método para salvar uma variável de ambiente usando 'setx'
        /// </summary>
        /// <param name="variableName"></param>
        /// <param name="value"></param>
        /// <param name="forAllUsers"></param>
        static void SaveEnvironmentVariable(string variableName, string value, bool forAllUsers)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                using (StreamWriter sw = process.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        // Monta o comando setx com ou sem o /M baseado na flag forAllUsers
                        string command = $"setx {variableName} \"{value}\"" + ( " /M" );
                        sw.WriteLine(command);
                    }
                }

                // Captura a saída para verificar erros ou confirmar sucesso
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (!string.IsNullOrEmpty(errors))
                {
                    Console.WriteLine("Erro ao definir a variável de ambiente:");
                    Console.WriteLine(errors);
                }
                else
                {
                    Console.WriteLine("Variável de ambiente definida com sucesso:");
                    Console.WriteLine(output);
                }
            }
        }
    }
}
