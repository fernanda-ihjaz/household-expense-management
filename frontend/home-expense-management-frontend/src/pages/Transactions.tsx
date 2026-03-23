import React, { useState } from "react";
import { useTransactions } from "../hooks/useTransactions";
import { useAppContext } from "../context/AppContext";
import { Button } from "../components/atoms/Button";
import { Badge } from "../components/atoms/Badge";
import { StatCard } from "../components/atoms/StatCard";
import { EmptyState } from "../components/molecules/EmptyState";
import { Modal } from "../components/molecules/Modal";
import { DataTable, type ColumnDef } from "../components/molecules/DataTable";
import { TransactionForm } from "../components/organisms/TransactionForm";
import type { Transaction } from "../types";
import { formatCurrency } from "../utils/helpers";

type TabType = "list" | "person" | "category";

export const Transactions: React.FC = () => {
  const {
    transactions,
    addTransaction,
    totalIncome,
    totalExpense,
    netBalance,
    summaryByPerson,
    summaryByCategory,
  } = useTransactions();

  const { people, categories } = useAppContext();
  const [showForm, setShowForm] = useState(false);
  const [tab, setTab] = useState<TabType>("list");

  const saveTransaction = async (data: Omit<Transaction, "id">) => {
    await addTransaction(data);
    setShowForm(false);
  };

  const listColumns: ColumnDef<Transaction>[] = [
    {
      header: "Descrição",
      render: (t) => (
        <span style={{ fontWeight: 500, color: "#0f172a" }}>
          {t.description}
        </span>
      ),
    },
    {
      header: "Pessoa",
      render: (t) => (
        <span style={{ color: "#64748b" }}>
          {people.find((p) => p.id === t.personId)?.name || "—"}
        </span>
      ),
    },
    {
      header: "Categoria",
      render: (t) => (
        <span style={{ color: "#191a1b" }}>
          {categories.find((c) => c.id === t.categoryId)?.description || "—"}
        </span>
      ),
    },
    {
      header: "Tipo",
      render: (t) => <Badge type={t.type} />,
    },
    {
      header: "Valor",
      render: (t) => (
        <span
          style={{
            fontWeight: 700,
            color: t.type === "Income" ? "#16a34a" : "#dc2626",
          }}
        >
          {formatCurrency(t.amount)}
        </span>
      ),
    },
  ];

  const summaryColumns: ColumnDef<any>[] = [
    {
      header: tab === "person" ? "Nome" : "Categoria",
      render: (s) => (
        <div>
          <div style={{ fontWeight: 600 }}>{s.name}</div>
          <div style={{ fontSize: 12, color: "#64748b" }}>
            {s.subtext}
          </div>
        </div>
      ),
    },
    {
      header: "Receita",
      render: (s) => (
        <span style={{ color: "#16a34a", fontWeight: 600 }}>
          {formatCurrency(s.Income)}
        </span>
      ),
    },
    {
      header: "Despesa",
      render: (s) => (
        <span style={{ color: "#dc2626", fontWeight: 600 }}>
          {formatCurrency(s.expense)}
        </span>
      ),
    },
    {
      header: "Saldo",
      render: (s) => (
        <span
          style={{
            color: s.balance >= 0 ? "#2563eb" : "#dc2626",
            fontWeight: 700,
          }}
        >
          {formatCurrency(s.balance)}
        </span>
      ),
    },
  ];

  const summaryFooter = (
    <tr style={{ background: "#f8fafc", fontWeight: 700 }}>
      <td style={{ padding: 12 }}>Total Geral</td>
      <td style={{ color: "#16a34a" }}>{formatCurrency(totalIncome)}</td>
      <td style={{ color: "#dc2626" }}>{formatCurrency(totalExpense)}</td>
      <td style={{ color: netBalance >= 0 ? "#2563eb" : "#dc2626" }}>
        {formatCurrency(netBalance)}
      </td>
    </tr>
  );

  const renderTabButton = (id: TabType, label: string) => (
  <button
    onClick={() => setTab(id)}
    style={{
      border: "none",
      borderRadius: 6,
      padding: "7px 18px",
      cursor: "pointer",
      fontWeight: 600,
      fontSize: 13,
      background: tab === id ? "#fff" : "transparent",
      color: tab === id ? "#2563eb" : "#64748b",
      boxShadow: tab === id ? "0 1px 4px rgba(0,0,0,.1)" : "none",
      transition: "all .15s",
    }}
  >
    {label}
  </button>
);

  return (
    <div>
      <div style={{ display: "flex", justifyContent: "space-between", alignItems: "flex-start", marginBottom: 28 }}>
        <div>
          <h1 style={{ margin: 0, fontSize: 26, fontWeight: 800, color: "#0f172a" }}>
            Transações
          </h1>
          <p style={{ margin: "4px 0 0", color: "#64748b", fontSize: 14 }}>
            Registre receitas e despesas
          </p>
        </div>
        <Button onClick={() => setShowForm(true)}>+ Nova Transação</Button>
      </div>

      <div style={{
        display: "flex",
        gap: 0,
        marginBottom: 20,
        background: "#f1f5f9",
        borderRadius: 8,
        padding: 3,
        width: "fit-content"
      }}>
        {renderTabButton("list", "Transações")}
        {renderTabButton("person", "Por Pessoa")}
        {renderTabButton("category", "Por Categoria")}
      </div>

      {tab === "list" && (
        <div style={{ background: "#fff", borderRadius: 16, border: "1.5px solid #e2e8f0", padding: 24 }}>
          <div style={{ marginBottom: 16 }}>
            <div style={{ fontWeight: 700, fontSize: 16, color: "#0f172a" }}>
              Transações Registradas
            </div>
            <div style={{ fontSize: 13, color: "#64748b" }}>
              {transactions.length} {transactions.length === 1 ? "transação" : "transações"}
            </div>
          </div>

          {transactions.length === 0 ? (
            <EmptyState
              icon=""
              message="Nenhuma transação registrada"
              hint="Clique em 'Nova Transação' para começar"
            />
          ) : (
            <DataTable data={transactions} columns={listColumns} keyExtractor={(t) => t.id} />
          )}
        </div>
      )}

      {(tab === "person" || tab === "category") && (
        <div>
          <div style={{ display: "flex", gap: 14, marginBottom: 20, flexWrap: "wrap" }}>
            <StatCard label="Receita Total" value={totalIncome} type="Income" />
            <StatCard label="Despesas Totais" value={totalExpense} type="Expense" />
            <StatCard label="Saldo Líquido Total" value={netBalance} type="Balance" />
          </div>

          <div style={{ background: "#fff", borderRadius: 16, border: "1.5px solid #e2e8f0", padding: 24 }}>
            <DataTable
              data={tab === "person" ? summaryByPerson : summaryByCategory}
              columns={summaryColumns}
              keyExtractor={(s) => s.id}
              footer={summaryFooter}
            />
          </div>
        </div>
      )}

      {showForm && (
        <Modal title="Nova Transação" onClose={() => setShowForm(false)}>
          <TransactionForm
            people={people}
            categories={categories}
            onSave={saveTransaction}
            onCancel={() => setShowForm(false)}
          />
        </Modal>
      )}
    </div>
  );
};