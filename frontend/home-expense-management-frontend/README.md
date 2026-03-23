# Household Expense Management — Frontend

Interface web para o sistema de gerenciamento de gastos domésticos, desenvolvida em **React** com **TypeScript**.

## Sobre o Projeto

A interface permite ao usuário gerenciar:
- *categorias*
- *pessoas*
- *transações financeiras*

Toda a lógica de negócio e persistência fica na API ("../backend"). O frontend consome os endpoints e trata os dados para exibição.

## Tecnologias

- React
- TypeScript
- Vite
- Lucide React *(ícones)*

## Decisões de Projeto

Estas são as decisões que tomei ao longo do desenvolvimento.

### Atomic Design

Escolhi o Atomic Design para organizar os componentes porque ele define uma hierarquia clara e organizada entre os componentes e eu já tenho familiaridade com esse padrão.

### Camada de Service

Criei uma camada de service separada para isolar toda a comunicação com a API. Os componentes nunca fazem `fetch` diretamente — eles chamam métodos como `transactionService.create()` ou `personService.getAll()`.

## Como Executar

### Pré-requisitos

- [Node.js](https://nodejs.org/) 18 ou superior
- A API do backend rodando localmente

### Passos

```bash
# 1. Clone o repositório
git clone <url-do-repositorio>
cd household-expense-management-frontend

# 2. Instale as dependências
npm install

# 3. Configure a URL da API
cp .env.example .env.local
```

Edite o `.env.local` com o endereço da API:

```env
VITE_API_BASE_URL=https://localhost:{porta}/api
```

```bash
# 4. Inicie o servidor de desenvolvimento
npm run dev
```

A interface estará disponível em `http://localhost:5173`.

## Estrutura de Pastas
 
```
src/
├── components/
│   ├── atoms/          # Badge, Button, Input, Select, StatCard
│   ├── molecules/      # ConfirmDialog, DataTable, EmptyState, Modal
│   ├── organisms/      # CategoryForm, PersonForm, Sidebar, TransactionForm
│   └── templates/
├── constants/
│   └── api.ts          # URL base da API
├── context/
│   └── AppContext.tsx  # Estado global da aplicação
├── hooks/
│   ├── useCategories.ts
│   ├── usePeople.ts
│   └── useTransactions.ts
├── pages/              # Categories, Dashboard, Persons, Transactions
├── services/           # api.ts, categoryService.ts, personService.ts, transactionService.ts
├── types/
│   └── index.ts        # Tipos de domínio e helpers de conversão
├── utils/
│   ├── formatters.ts
│   └── helpers.ts
├── App.tsx
├── AppInitializer.tsx
└── main.tsx
```


## Build

```bash
# Verificar tipos e gerar o build de produção
npm run build

# Pré-visualizar o build localmente
npm run preview
```