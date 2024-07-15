# SetEnvApp

`SetEnvApp` é uma aplicação console em C# destinada a facilitar a definição de variáveis de ambiente no Windows através do comando `setx`. O aplicativo permite definir variáveis tanto para o usuário atual quanto para todos os usuários (requerendo privilégios de administrador para o último).

## Estrutura do Código

### Program.cs

O arquivo `Program.cs` contém o ponto de entrada da aplicação e o método responsável por definir as variáveis de ambiente.

#### Classe `Program`

- **Método `Main`**: É o ponto de entrada da aplicação. Este método chama `SaveEnvironmentVariable` para salvar uma variável de ambiente.

```csharp
static void Main(string[] args)
{
    SaveEnvironmentVariable("MINHAVARIAVEL", "Hello World", forAllUsers: true);
}
```

- **Método `SaveEnvironmentVariable`**: Este método cria um processo que executa o comando `setx` para definir uma variável de ambiente. Ele aceita três parâmetros:
  - `variableName`: Nome da variável de ambiente.
  - `value`: Valor atribuído à variável.
  - `forAllUsers`: Booleano que, se verdadeiro, define a variável para todos os usuários no sistema (requer privilégios de administrador).

```csharp
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
                string command = $"setx {variableName} \"{value}\"" + (forAllUsers ? " /M" : "");
                sw.WriteLine(command);
            }
        }

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
```

## Configuração do Projeto

### Requisitos

- Windows 10 ou superior.
- .NET 8.0 ou superior.

### Como Executar

1. **Clonar o Repositório** (assumindo que o projeto esteja disponível em um repositório Git):
   ```bash
   git clone [URL do repositório]
   cd SetEnvApp
   ```

2. **Compilar o Projeto**:
   ```bash
   dotnet build
   ```

3. **Executar a Aplicação**:
   - **Como Administrador**: Para definir variáveis de ambiente para todos os usuários, execute o aplicativo como administrador.
   ```bash
   dotnet run
   ```

## Problemas Comuns

- **Permissões de Administrador**: Se tentar definir uma variável para todos os usuários sem privilégios de administrador, você receberá um erro.
- **Variável Não Visível Imediatamente**: Lembre-se de que as variáveis definidas com `setx` só estarão visíveis em novos processos.

Este README oferece uma visão geral do projeto, explicando o propósito e funcionamento do código, além de guiar novos usuários sobre como configurar e utilizar a aplicação.
