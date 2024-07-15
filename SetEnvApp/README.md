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

Detalhes sobre o uso de `Process.StartInfo` ajudará a compreender melhor como o método `SaveEnvironmentVariable` configura e executa um processo externo, neste caso, o `cmd.exe` para executar o comando `setx`. Aqui estão os detalhes sobre cada propriedade utilizada:

### Detalhes do `Process.StartInfo`

O objeto `ProcessStartInfo` é utilizado para configurar novos processos. No método `SaveEnvironmentVariable`, ele é configurado para iniciar o `cmd.exe` com parâmetros específicos que permitem a execução do comando `setx`. Aqui estão as propriedades utilizadas e suas descrições:

- **`FileName`**: Define o nome do arquivo executável para o processo a ser iniciado. Aqui, ele é definido como `"cmd.exe"`, que é o interpretador de linha de comando do Windows.

  ```csharp
  process.StartInfo.FileName = "cmd.exe";
  ```

- **`UseShellExecute`**: Indica se o processo deve ser iniciado através do shell do sistema operacional. Quando `false`, permite a redireção de entrada/saída e é necessário se você deseja capturar a saída do processo.

  ```csharp
  process.StartInfo.UseShellExecute = false;
  ```

- **`RedirectStandardInput`**: Permite que o programa escreva no fluxo de entrada do processo. Isso é usado aqui para enviar comandos para o `cmd.exe`.

  ```csharp
  process.StartInfo.RedirectStandardInput = true;
  ```

- **`RedirectStandardOutput`**: Permite que o programa leia o fluxo de saída padrão (stdout) do processo. Isso é usado para capturar a saída do comando `setx`.

  ```csharp
  process.StartInfo.RedirectStandardOutput = true;
  ```

- **`RedirectStandardError`**: Permite que o programa leia o fluxo de erro padrão (stderr) do processo. Isso é útil para capturar e exibir mensagens de erro.

  ```csharp
  process.StartInfo.RedirectStandardError = true;
  ```

- **`CreateNoWindow`**: Quando definido como `true`, impede que a janela do console seja mostrada quando o comando é executado. Isso é útil para processos em segundo plano onde uma interface gráfica não é necessária.

  ```csharp
  process.StartInfo.CreateNoWindow = true;
  ```

### Uso no Método `SaveEnvironmentVariable`

Quando o método `SaveEnvironmentVariable` é chamado, ele configura um novo processo com essas propriedades, inicia o `cmd.exe`, e usa a entrada padrão para passar o comando `setx` ao shell. O comando depende da variável `forAllUsers` para decidir se adiciona o parâmetro `/M` para definir a variável de ambiente em nível de sistema.

Aqui está a parte relevante do código, incluindo a montagem do comando e a escrita na entrada padrão:

```csharp
using (StreamWriter sw = process.StandardInput)
{
    if (sw.BaseStream.CanWrite)
    {
        // Adiciona "/M" ao comando se forAllUsers for true
        string command = $"setx {variableName} \"{value}\"" + (forAllUsers ? " /M" : "");
        sw.WriteLine(command);
    }
}
```

Após a execução do comando, o programa lê as saídas padrão e de erro para verificar se o comando foi bem-sucedido ou se ocorreram erros.



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
