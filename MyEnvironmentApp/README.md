# MyEnvironmentApp

`MyEnvironmentApp` é uma aplicação console em C# projetada para ler e exibir variáveis de ambiente do sistema operacional. A aplicação verifica a existência de uma variável de ambiente específica e lista todas as variáveis de ambiente disponíveis.

## Estrutura do Código

### Program.cs

Este arquivo contém o código principal da aplicação, incluindo a lógica para ler e exibir variáveis de ambiente.

#### Classe `Program`

- **Método `Main`**: É o ponto de entrada da aplicação. Este método executa duas funções principais:
  - Lê uma variável de ambiente específica.
  - Lista todas as variáveis de ambiente do sistema.

```csharp
static void Main(string[] args)
{
    string myVariable = Environment.GetEnvironmentVariable("MINHAVARIAVEL", EnvironmentVariableTarget.Machine);

    if (string.IsNullOrEmpty(myVariable))
    {
        Console.WriteLine("A variável de ambiente 'MINHA_VARIAVEL' não está definida.");
    }
    else
    {
        Console.WriteLine($"Valor de 'MINHA_VARIAVEL': {myVariable}");
    }

    Console.WriteLine("\nTodas as variáveis de ambiente:");
    foreach (System.Collections.DictionaryEntry env in Environment.GetEnvironmentVariables())
    {
        Console.WriteLine($"{env.Key} = {env.Value}");
    }
}
```

### Detalhes Técnicos

- **`Environment.GetEnvironmentVariable`**: Este método é usado para obter o valor de uma variável de ambiente. O parâmetro `EnvironmentVariableTarget.Machine` indica que a variável de ambiente é buscada no contexto da máquina, ou seja, é uma variável de ambiente do sistema.

- **`Environment.GetEnvironmentVariables`**: Este método retorna todas as variáveis de ambiente do sistema como uma coleção, que é então percorrida para imprimir cada chave e valor.

 O método `Environment.GetEnvironmentVariable` e o enum `EnvironmentVariableTarget` são usados em .NET para acessar variáveis de ambiente de maneiras específicas. Vamos detalhar cada um deles para entender melhor seu funcionamento e uso.

### `Environment.GetEnvironmentVariable`

Este método é utilizado para recuperar o valor de uma variável de ambiente específica. Ele pode ser invocado de duas maneiras: passando apenas o nome da variável ou passando o nome e um especificador de destino. O método possui duas sobrecargas principais:

1. **`Environment.GetEnvironmentVariable(string variable)`**:
   - Retorna o valor de uma variável de ambiente do processo atual.

2. **`Environment.GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)`**:
   - Retorna o valor de uma variável de ambiente com base no `target` especificado, que pode ser para o processo atual, para todos os usuários no computador ou para o usuário atual específico.

### `EnvironmentVariableTarget`

Este enum é usado para especificar o local de uma variável de ambiente no sistema. Ele tem três valores possíveis:

- **`EnvironmentVariableTarget.Process`**:
  - O valor da variável é buscado apenas no contexto do processo atual. Alterações feitas às variáveis de ambiente com este alvo não são persistentes e são visíveis apenas para o processo atual.

- **`EnvironmentVariableTarget.User`**:
  - O valor da variável é buscado no contexto do usuário atualmente logado. Variáveis definidas com esse alvo são armazenadas e disponíveis para todos os processos iniciados pelo usuário atual, mas não são visíveis para outros usuários do sistema.

- **`EnvironmentVariableTarget.Machine`**:
  - O valor da variável é buscado no contexto de todo o sistema. Variáveis definidas com esse alvo são armazenadas de forma a serem disponíveis para todos os usuários e processos no computador. Isso requer privilégios administrativos para escrever, mas qualquer processo pode ler essas variáveis.

### Uso do `Environment.GetEnvironmentVariable`

Quando você chama `Environment.GetEnvironmentVariable("MINHAVARIAVEL", EnvironmentVariableTarget.Machine)`, o sistema busca o valor da variável de ambiente chamada `MINHAVARIAVEL` que está armazenada no contexto do sistema (nível de máquina). Isso é útil para ler configurações que devem ser aplicadas universalmente a todos os usuários e processos em um computador.

### Considerações de Segurança

- **Leitura de Variáveis**: A leitura de variáveis de ambiente geralmente não apresenta riscos de segurança, mas dependendo do conteúdo da variável, informações sensíveis podem ser expostas a processos ou usuários que não deveriam ter acesso a elas.

- **Escrita em `Machine`**: Escrever em `EnvironmentVariableTarget.Machine` pode exigir privilégios administrativos, e alterações são visíveis para todos os usuários e processos. Cuidado deve ser tomado para não sobrescrever variáveis críticas do sistema ou expor dados sensíveis inapropriadamente.

### Exemplo de Uso

```csharp
string myVariable = Environment.GetEnvironmentVariable("MINHAVARIAVEL", EnvironmentVariableTarget.Machine);

if (string.IsNullOrEmpty(myVariable))
{
    Console.WriteLine("A variável de ambiente 'MINHAVARIAVEL' não está definida.");
}
else
{
    Console.WriteLine($"Valor de 'MINHAVARIAVEL': {myVariable}");
}
```

Este exemplo demonstra como acessar uma variável de ambiente global de forma programática em C#, facilitando a obtenção de configurações ou dados relevantes que são comuns a todos os usuários e processos no sistema. 

## Configuração do Projeto

### Requisitos

- .NET Core 3.1 ou superior.
- IDE compatível com C# (Recomendado: Visual Studio, Visual Studio Code com extensão C#).

### Como Compilar e Executar

1. **Clonar o Repositório** (assumindo que o projeto esteja disponível em um repositório Git):
   ```bash
   git clone [URL do repositório]
   cd MyEnvironmentApp
   ```

2. **Compilar o Projeto**:
   ```bash
   dotnet build
   ```

3. **Executar a Aplicação**:
   ```bash
   dotnet run
   ```

## Problemas Comuns

- **Variável de Ambiente Não Encontrada**: Se a variável especificada não for encontrada, a aplicação informará que a variável de ambiente 'MINHA_VARIAVEL' não está definida. Certifique-se de que a variável está corretamente configurada no sistema antes de executar a aplicação.

Este README oferece uma visão completa sobre como a aplicação `MyEnvironmentApp` funciona, incluindo explicações detalhadas sobre cada parte do código e instruções sobre como configurar e executar a aplicação.
