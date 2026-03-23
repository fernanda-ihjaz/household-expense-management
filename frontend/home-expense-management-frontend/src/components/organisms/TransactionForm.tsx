import React, { useState, useMemo } from "react";
import { Input } from "../atoms/Input";
import { Select } from "../atoms/Select";
import { Button } from "../atoms/Button";
import type { Person, Category, Transaction, TransactionType } from "../../types";
import { typeStringToId } from "../../types";

interface TransactionFormData {
  description: string;
  amount: string;
  type: string;
  categoryId: string;
  personId: string;
}

interface TransactionFormProps {
  people: Person[];
  categories: Category[];
  onSave: (data: Omit<Transaction, "id">) => void;
  onCancel: () => void;
}

export const TransactionForm: React.FC<TransactionFormProps> = ({
  people,
  categories,
  onSave,
  onCancel,
}) => {
  const [form, setForm] = useState<TransactionFormData>({
    description: "",
    amount: "",
    type: "",
    categoryId: "",
    personId: "",
  });

  const [errors, setErrors] = useState<Partial<TransactionFormData>>({});

  const selectedPerson = people.find((p) => p.id === form.personId);
  const isMinor = selectedPerson && selectedPerson.age < 18;

const availableCats = useMemo(() => {
  if (!form.type) return categories;

  return categories.filter(
    (c) =>
      c.purpose === "Both" ||
      c.purpose === form.type
  );
}, [form.type, categories]);

  const validate = () => {
    const e: Partial<TransactionFormData> = {};

    if (!form.description.trim())
      e.description = "Descrição é obrigatória";

    if (form.description.length > 400)
      e.description = "Máximo de 400 caracteres";

    const val = parseFloat(form.amount);
    if (!form.amount || isNaN(val) || val <= 0)
      e.amount = "O valor deve ser maior que zero";

    if (!form.type)
      e.type = "Tipo é obrigatório";

    if (isMinor && form.type === "Income")
      e.type = "Menores só podem ter despesas";

    if (!form.personId)
      e.personId = "Pessoa é obrigatória";

    if (!form.categoryId)
      e.categoryId = "Categoria é obrigatória";

    return e;
  };

  const handleSave = () => {
    const e = validate();

    if (Object.keys(e).length) {
      setErrors(e);
      return;
    }

    onSave({
      description: form.description.trim(),
      amount: parseFloat(form.amount),
      type: form.type as TransactionType,
      typeId: typeStringToId[form.type as TransactionType],
      categoryId: form.categoryId,
      personId: form.personId,
    });
  };

  const handleTypeChange = (v: string) => {
    setForm((f) => ({
      ...f,
      type: v,
      categoryId: "",
    }));
  };

  return (
    <div style={{ display: "flex", flexDirection: "column", gap: 16 }}>
      <Input
        label="Descrição"
        value={form.description}
        onChange={(v) =>
          setForm((f) => ({ ...f, description: v }))
        }
        placeholder="Descreva a transação"
        maxLength={400}
        required
        error={errors.description}
      />

      <Input
        label="Valor (R$)"
        value={form.amount}
        onChange={(v) =>
          setForm((f) => ({ ...f, amount: v }))
        }
        type="number"
        placeholder="0.00"
        required
        error={errors.amount}
      />

      <Select
        label="Pessoa"
        value={form.personId}
        onChange={(v) =>
          setForm((f) => ({
            ...f,
            personId: v,
            type: "",
            categoryId: "",
          }))
        }
        placeholder="Selecione a pessoa"
        required
        error={errors.personId}
        options={people.map((p) => ({
          value: p.id,
          label: `${p.name} (${p.age} anos)`,
        }))}
      />

      <Select
        label="Tipo"
        value={form.type}
        onChange={handleTypeChange}
        placeholder="Selecione o tipo"
        required
        error={errors.type}
        options={[
          { value: "Income", label: "Receita", disabled: !!isMinor },
          { value: "Expense", label: "Despesa" },
        ].filter((o) => !o.disabled || o.value === "Expense")}
      />

      {isMinor && (
        <div
          style={{
            fontSize: 12,
            color: "#b45309",
            background: "#fef3c7",
            borderRadius: 8,
            padding: "8px 12px",
          }}
        >
          ⚠️ Menor de idade: apenas despesas são permitidas.
        </div>
      )}

      <Select
        label="Categoria"
        value={form.categoryId}
        onChange={(v) =>
          setForm((f) => ({ ...f, categoryId: v }))
        }
        placeholder="Selecione a categoria"
        required
        error={errors.categoryId}
        options={availableCats.map((c) => ({
          value: c.id,
          label: `${c.description} (${c.purpose})`,
        }))}
      />

      <div
        style={{
          display: "flex",
          justifyContent: "flex-end",
          gap: 10,
          marginTop: 8,
        }}
      >
        <Button variant="ghost" onClick={onCancel}>
          Cancelar
        </Button>
        <Button onClick={handleSave}>
          Salvar
        </Button>
      </div>
    </div>
  );
};