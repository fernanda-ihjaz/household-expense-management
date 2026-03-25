# Household Expense Management

Sistema para gerenciamento de gastos domésticos. Permite registrar pessoas, categorias e transações financeiras, além de visualizar relatórios de receitas, despesas e saldo por pessoa e por categoria.

O projeto é dividido em duas partes, cada uma com um README próprio documentando as decisões de projeto, tecnologias utilizadas, pré-requisitos e instruções detalhadas para execução:

```
/
├── backend/    # API REST em .NET 9
└── frontend/   # Interface web em React + TypeScript
```

- [`backend/README.md`](./backend/README.md) — documentação da API, arquitetura, padrões adotados e como rodar o backend
- [`frontend/README.md`](./frontend/README.md) — documentação da interface, estrutura de componentes, decisões de projeto e como rodar o frontend

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Node.js](https://nodejs.org/) 18 ou superior
- EF Core CLI:

```bash
dotnet tool install --global dotnet-ef
```

##### Desenvolvido por Fernanda Japur Ihjaz
