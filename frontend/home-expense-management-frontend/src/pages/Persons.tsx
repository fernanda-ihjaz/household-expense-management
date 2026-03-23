import React, { useState } from "react";
import { usePeople } from "../hooks/usePeople";
import { useAppContext } from "../context/AppContext";
import { Button } from "../components/atoms/Button";
import { EmptyState } from "../components/molecules/EmptyState";
import { ConfirmDialog } from "../components/molecules/ConfirmDialog";
import { Modal } from "../components/molecules/Modal";
import { DataTable, type ColumnDef } from "../components/molecules/DataTable";
import { PersonForm } from "../components/organisms/PersonForm";
import { type Person } from "../types";

export const People: React.FC = () => {
  const { people, addPerson, editPerson, deletePerson } = usePeople();
  const { transactions } = useAppContext();

  const [showForm, setShowForm] = useState(false);
  const [editTarget, setEditTarget] = useState<Person | null>(null);
  const [confirmDelete, setConfirmDelete] = useState<Person | null>(null);

  const openNew = () => {
    setEditTarget(null);
    setShowForm(true);
  };

  const openEdit = (p: Person) => {
    setEditTarget(p);
    setShowForm(true);
  };

  const savePerson = async (data: Omit<Person, "id">) => {
    if (editTarget) {
      await editPerson(editTarget.id, data);
    } else {
      await addPerson(data);
    }
    setShowForm(false);
  };

  // FIX 1: fecha o dialog após confirmar a exclusão
  const handleConfirmDelete = async () => {
    if (!confirmDelete) return;
    await deletePerson(confirmDelete.id);
    setConfirmDelete(null);
  };

  const columns: ColumnDef<Person>[] = [
    {
      header: "Nome",
      render: (p) => (
        <div>
          <div style={{ fontWeight: 600, color: "#0f172a" }}>{p.name}</div>
          {p.age < 18 && (
            <span
              style={{
                fontSize: 11,
                background: "#fef3c7",
                color: "#b45309",
                borderRadius: 12,
                padding: "1px 8px",
                fontWeight: 600,
              }}
            >
              Menor de idade
            </span>
          )}
        </div>
      ),
    },
    {
      header: "Idade",
      render: (p) => (
        <span style={{ color: "#64748b" }}>{p.age} anos</span>
      ),
    },
    {
      header: "Transações",
      render: (p) => (
        <span style={{ color: "#64748b" }}>
          {transactions.filter((t) => t.personId === p.id).length}
        </span>
      ),
    },
    {
      header: "Ações",
      render: (p) => (
        <div style={{ display: "flex", gap: 8 }}>
          <Button small variant="ghost" onClick={() => openEdit(p)}>
            Editar
          </Button>
          <Button small variant="danger" onClick={() => setConfirmDelete(p)}>
            Excluir
          </Button>
        </div>
      ),
    },
  ];

  return (
    <div>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "flex-start",
          marginBottom: 28,
        }}
      >
        <div>
          <h1 style={{ margin: 0, fontSize: 26, fontWeight: 800, color: "#0f172a" }}>
            Pessoas
          </h1>
          <p style={{ margin: "4px 0 0", color: "#64748b", fontSize: 14 }}>
            Membros do grupo familiar
          </p>
        </div>
        <Button onClick={openNew}>+ Nova Pessoa</Button>
      </div>

      <div
        style={{
          background: "#fff",
          borderRadius: 16,
          border: "1.5px solid #e2e8f0",
          padding: 24,
        }}
      >
        <div style={{ marginBottom: 16 }}>
          <div style={{ fontWeight: 700, fontSize: 16, color: "#0f172a" }}>
            Pessoas Registradas
          </div>
          <div style={{ fontSize: 13, color: "#64748b" }}>
            {people.length} {people.length === 1 ? "pessoa" : "pessoas"}
          </div>
        </div>

        {people.length === 0 ? (
          <EmptyState
            icon=""
            message="Nenhuma pessoa registrada"
            hint="Clique em 'Nova Pessoa' para registrar alguém"
          />
        ) : (
          <DataTable data={people} columns={columns} keyExtractor={(p) => p.id} />
        )}
      </div>

      {showForm && (
        <Modal
          title={editTarget ? "Editar Pessoa" : "Registrar Nova Pessoa"}
          onClose={() => setShowForm(false)}
        >
          <PersonForm
            initialData={editTarget}
            onSave={savePerson}
            onCancel={() => setShowForm(false)}
          />
        </Modal>
      )}

      {confirmDelete && (
        <ConfirmDialog
          message={`Excluir "${confirmDelete.name}"? Todas as transações dessa pessoa serão removidas.`}
          onConfirm={handleConfirmDelete}
          onCancel={() => setConfirmDelete(null)}
        />
      )}
    </div>
  );
};