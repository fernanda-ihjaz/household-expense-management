import React, { useState, useEffect } from "react";
import { Input } from "../atoms/Input";
import { Button } from "../atoms/Button";
import type { Person } from "../../types";

interface PersonFormData {
  name: string;
  age: string;
}

interface PersonFormProps {
  initialData?: Person | null;
  onSave: (data: Omit<Person, "id">) => void;
  onCancel: () => void;
}

export const PersonForm: React.FC<PersonFormProps> = ({ initialData, onSave, onCancel }) => {
  const [form, setForm] = useState<PersonFormData>({ name: "", age: "" });
  const [errors, setErrors] = useState<Partial<PersonFormData>>({});

  useEffect(() => {
    if (initialData) {
      setForm({ name: initialData.name, age: String(initialData.age) });
    }
  }, [initialData]);

  const validate = () => {
    const e: Partial<PersonFormData> = {};
    if (!form.name.trim()) e.name = "Nome é obrigatório";
    if (form.name.length > 200) e.name = "Máximo de 200 caracteres";
    const age = parseInt(form.age);
    if (!form.age || isNaN(age) || age < 0) e.age = "Idade inválida";
    return e;
  };

  const handleSave = () => {
    const e = validate();
    if (Object.keys(e).length) {
      setErrors(e);
      return;
    }
    onSave({
      name: form.name.trim(),
      age: parseInt(form.age),
    });
  };

  return (
    <div style={{ display: "flex", flexDirection: "column", gap: 16 }}>
      <Input
        label="Nome"
        value={form.name}
        onChange={(v) => setForm((f) => ({ ...f, name: v }))}
        placeholder="Digite o nome"
        maxLength={200}
        required
        error={errors.name}
      />
      <Input
        label="Idade"
        value={form.age}
        onChange={(v) => setForm((f) => ({ ...f, age: v }))}
        type="number"
        placeholder="Digite a idade"
        required
        error={errors.age}
      />
      <div style={{ display: "flex", justifyContent: "flex-end", gap: 10, marginTop: 8 }}>
        <Button variant="ghost" onClick={onCancel}>Cancelar</Button>
        <Button onClick={handleSave}>Salvar</Button>
      </div>
    </div>
  );
};