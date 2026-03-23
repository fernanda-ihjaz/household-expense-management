import { useAppContext } from "../context/AppContext";
import { StatCard } from "../components/atoms/StatCard";

export const Dashboard = () => {
  const { people, categories, transactions } = useAppContext();

  const totalIncome = transactions
    .filter((t) => t.type === "Income")
    .reduce((s, t) => s + t.amount, 0);

  const totalExpense = transactions
    .filter((t) => t.type === "Expense")
    .reduce((s, t) => s + t.amount, 0);

  const netBalance = totalIncome - totalExpense;

  return (
    <div>
      <div style={{ marginBottom: 28 }}>
        <h1 style={{ margin: 0, fontSize: 26, fontWeight: 800, color: "#0f172a" }}>Visão Geral</h1>
        <p style={{ margin: "4px 0 0", color: "#64748b", fontSize: 14 }}>Resumo da sua residência</p>
      </div>

      <div style={{ display: "grid", gridTemplateColumns: "repeat(auto-fit, minmax(140px, 1fr))", gap: 14, marginBottom: 20 }}>
        {[
          { label: "Total de Pessoas", val: people.length },
          { label: "Categorias", val: categories.length },
          { label: "Transações", val: transactions.length },
        ].map((k) => (
          <div key={k.label} style={{ background: "#fff", borderRadius: 12, padding: "16px 20px", border: "1.5px solid #e2e8f0", display: "flex", justifyContent: "space-between", alignItems: "center" }}>
            <div>
              <div style={{ fontSize: 12, color: "#64748b", fontWeight: 500 }}>{k.label}</div>
              <div style={{ fontSize: 24, fontWeight: 700, color: "#0f172a" }}>{k.val}</div>
            </div>
            <span style={{ fontSize: 24 }}></span>
          </div>
        ))}
      </div>

      <div style={{ display: "flex", gap: 14, marginBottom: 24, flexWrap: "wrap" }}>
        <StatCard label="Total de Receitas" value={totalIncome} type="Income" />
        <StatCard label="Total de Despesas" value={totalExpense} type="Expense" />
        <StatCard label="Saldo Líquido" value={netBalance} type="Balance" />
      </div>
    </div>
  );
};