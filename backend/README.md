# Household Expense Management

API para gerenciamento de gastos domésticos, desenvolvida em **.NET 9**.

## Sobre o Projeto

A API permite gerenciar:
- *categorias*
- *pessoas*
- *transações financeiras*

Cada transação pode ser associada a uma pessoa e a uma categoria, possibilitando rastrear quem gastou o quê e em qual categoria.

## Tecnologias

- .NET 9
- ASP.NET Core 9 *(Camada API)*
- Entity Framework Core
- MediatR
- Swagger
- xUnit
- Moq
- FluentAssertions

## Decisões de Projeto

Estas são as decisões que tomei ao longo do desenvolvimento.

### .NET 9

Escolhi o .NET 9 por ser uma das versões mais recentes da plataforma e por ser a versão utilizada pela empresa à qual esse projeto se destina.

### DDD

Utilizei DDD para modelar a API. A principal razão foi a minha familiaridade com a abordagem, já tendo estudado e aplicado em outros projetos.

Outro fator foi a natureza do desafio: recebi as regras de negócio e as entidades logo no início, e opara mim DDD é uma forma clara de estruturar esse tipo de problema. Entidades, regras de negócio e serviços de domínio ficam bem definidos e separados desde o começo.


### PostgreSQL

Escolhi o PostgreSQL por ser um banco robusto, open source e amplamente utilizado no mercado.

## Padrões de Projeto

### CQRS com MediatR

O principal benefício foi manter os controllers completamente limpos, a ideia é que eles não tenham lógica alguma, apenas recebem a requisição e delegam para o handler correto via `IMediator`.

Um dos meus objetivos no projeto era ter uma camada de testes para os controllers, e o uso desse pattern também simplifica muito os testes. Para testar o controller, basta mockar o `IMediator`. Para testar a lógica de negócio, basta testar o handler diretamente, sem precisar subir contexto HTTP nem infraestrutura.

**Fluxo de uma requisição:**
```
POST /api/transactions
  → TransactionController.Create()
    → _mediator.Send(TransactionCreateCommandRequest)
      → TransactionCreateCommandHandler (lógica de negócio)
        → Retorna Result<Guid>
          → Controller decide o status HTTP (201 ou 400)
```

### Padrão Result

Os handlers retornam um `Result<T>` em vez de lançar exceções para erros esperados, como uma validação que falhou. O fluxo de sucesso e de falha fica explícito e previsível no controller:

```csharp
if (!result.IsSuccess)
    return BadRequest(result.Error);

return CreatedAtAction(nameof(Create), new { id }, new { id });
```

Dessa forma, a pessoa que estiver lendo esse código entende rapidamente os dois caminhos possíveis, sem precisar rastrear onde uma exceção pode ser lançada.

### Repository

Utilizei o padrão Repository para isolar o acesso ao banco de dados da lógica de negócio. Os handlers da camada `Application` dependem de interfaces de repositório, e a implementação concreta fica na camada `Infrastructure`.

Isso torna os handlers independentes do EF Core, pois eles não sabem e não precisam saber como os dados são persistidos.

## Como Executar

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) rodando localmente
- EF Core CLI — instale com:

```bash
dotnet tool install --global dotnet-ef
```

### Passos

```bash
# 1. Clone o repositório
git clone <url-do-repositorio>
cd HouseholdExpenseManagement
```
 
Configure a connection string no `appsettings.json` do projeto `Api` com os dados do seu banco, por padrão está assim:
 
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres"
}
```
 
```bash
# 2. Restaure as dependências
dotnet restore
 
# 3. Aplique as migrations para criar o banco de dados
# o comando deve ser executado de dentro da seguinte pasta
cd HouseholdExpenseManagement.Infrastructure

dotnet ef database update -s "..\HouseholdExpenseManagement.Api"
 
# 4. Na raiz do projeto, execute a API
cd ..
dotnet run --project HouseholdExpenseManagement.Api
```
 
A API estará disponível em `https://localhost:{porta}`.
A documentação Swagger estará em `https://localhost:{porta}/`, no caminho `/`.

---

## Banco de Dados e Migrations

A pasta `Migrations/` é versionada no Git, ou seja, ela contém o histórico de evolução do schema e é necessária para que o `database update` funcione ao clonar o projeto.

O arquivo do banco de dados (`.db`) está no `.gitignore` e não é commitado. Cada pessoa que clonar o projeto precisa rodar as migrations para criar o banco localmente.

### Comandos úteis

```bash
# Aplicar todas as migrations pendentes
dotnet ef database update --project HouseholdExpenseManagement.Infrastructure --startup-project HouseholdExpenseManagement.Api

# Criar uma nova migration após alterar o modelo
dotnet ef migrations add NomeDaMigration --project HouseholdExpenseManagement.Infrastructure --startup-project HouseholdExpenseManagement.Api

# Listar todas as migrations e seus status
dotnet ef migrations list --project HouseholdExpenseManagement.Infrastructure --startup-project HouseholdExpenseManagement.Api
```

---

## Testes

Os testes são unitários e cobrem os três controllers. Cada endpoint tem ao menos um cenário de sucesso e um de falha, validando o status code e o corpo da resposta.

O `IMediator` é mockado via **Moq**, então os testes são completamente isolados.

```bash
# Rodar todos os testes
dotnet test

# Rodar com detalhes de cada teste
dotnet test --logger "console;verbosity=detailed"