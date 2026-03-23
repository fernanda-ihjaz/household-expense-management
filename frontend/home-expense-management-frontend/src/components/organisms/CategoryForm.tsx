import React, { useState } from "react";
import { Input } from "../atoms/Input";
import { Select } from "../atoms/Select";
import { Button } from "../atoms/Button";
import type { Category, PurposeType } from "../../types";
import { purposeStringToId } from "../../types";

interface CategoryFormData {
  description: string;
  purpose: string;
}

interface CategoryFormProps {
  onSave: (data: Omit<Category, "id">) => void;
  onCancel: () => void;
}

export const CategoryForm: React.FC<CategoryFormProps> = ({ onSave, onCancel }) => {
  const [form, setForm] = useState<CategoryFormData>({ description: "", purpose: "" });
  const [errors, setErrors] = useState<Partial<CategoryFormData>>({});

  const validate = () => {
    const e: Partial<CategoryFormData> = {};
    if (!form.description.trim()) e.description = "Descrição é obrigatória";
    if (form.description.length > 400) e.description = "Máximo de 400 caracteres";
    if (!form.purpose) e.purpose = "Tipo é obrigatório";
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
      purpose:   form.purpose as PurposeType,
      purposeId: purposeStringToId[form.purpose as PurposeType],
    });
  };

  return (
    <div style={{ display: "flex", flexDirection: "column", gap: 16 }}>
      <Input
        label="Descrição"
        value={form.description}
        onChange={(v) => setForm((f) => ({ ...f, description: v }))}
        placeholder="ex: Salário, Alimentação, Transporte..."
        maxLength={400}
        required
        error={errors.description}
      />
      <Select
        label="Tipo"
        value={form.purpose}
        onChange={(v) => setForm((f) => ({ ...f, purpose: v }))}
        placeholder="Selecione o tipo"
        required
        error={errors.purpose}
        options={[
          { value: "Income", label: "Receita" },
          { value: "Expense", label: "Despesa" },
          { value: "Both", label: "Ambos" },
        ]}
      />
      <div style={{ display: "flex", justifyContent: "flex-end", gap: 10, marginTop: 8 }}>
        <Button variant="ghost" onClick={onCancel}>Cancelar</Button>
        <Button onClick={handleSave}>Salvar</Button>
      </div>
    </div>
  );
};