import React, { useState } from "react";
import { Button } from "../components/atoms/Button";
import { Badge } from "../components/atoms/Badge";
import { EmptyState } from "../components/molecules/EmptyState";
import { Modal } from "../components/molecules/Modal";
import { DataTable, type ColumnDef } from "../components/molecules/DataTable";
import { CategoryForm } from "../components/organisms/CategoryForm";
import type { Category } from "../types";
import { useCategories } from "../hooks/useCategories";

export const Categories: React.FC = () => {
  const { categories, addCategory } = useCategories();
  const [showForm, setShowForm] = useState(false);

  const saveCategory = async (data: Omit<Category, "id">) => {
    await addCategory(data);
    setShowForm(false);
  };

  const columns: ColumnDef<Category>[] = [
    {
      header: "Descrição",
      render: (c) => <span style={{ fontWeight: 500, color: "#0f172a" }}>{c.description}</span>,
    },
    {
      header: "Tipo",
      render: (c) => <Badge type={c.purpose} />,
    },
  ];

  return (
    <div>
      <div style={{ display: "flex", justifyContent: "space-between", alignItems: "flex-start", marginBottom: 28 }}>
        <div>
          <h1 style={{ margin: 0, fontSize: 26, fontWeight: 800, color: "#0f172a" }}>Categorias</h1>
          <p style={{ margin: "4px 0 0", color: "#64748b", fontSize: 14 }}>Organize suas receitas e despesas</p>
        </div>
        <Button onClick={() => setShowForm(true)}>+ Nova Categoria</Button>
      </div>

      <div style={{ background: "#fff", borderRadius: 16, border: "1.5px solid #e2e8f0", padding: 24 }}>
        <div style={{ marginBottom: 16 }}>
          <div style={{ fontWeight: 700, fontSize: 16, color: "#0f172a" }}>Categorias Cadastradas</div>
          <div style={{ fontSize: 13, color: "#64748b" }}>
            {categories.length} {categories.length === 1 ? "categoria" : "categorias"}
          </div>
        </div>

        {categories.length === 0 ? (
          <EmptyState icon="" message="Nenhuma categoria cadastrada" hint="Clique em 'Nova Categoria' para começar" />
        ) : (
          <DataTable data={categories} columns={columns} keyExtractor={(c) => c.id} />
        )}
      </div>

      {showForm && (
        <Modal title="Cadastrar Nova Categoria" onClose={() => setShowForm(false)}>
          <CategoryForm onSave={saveCategory} onCancel={() => setShowForm(false)} />
        </Modal>
      )}
    </div>
  );
};