import React from "react";
import type { TransactionType, PurposeType } from "../../types";

type BadgeType = TransactionType | PurposeType;

// FIX: labels em PT-BR
const labelMap: Record<string, string> = {
  Income:  "Receita",
  Expense: "Despesa",
  Both:    "Ambos",
};

const colorMap: Record<string, { background: string; color: string }> = {
  Income:  { background: "#dcfce7", color: "#16a34a" },
  Expense: { background: "#fee2e2", color: "#dc2626" },
  Both:    { background: "#dbeafe", color: "#2563eb" },
};

interface BadgeProps {
  type: BadgeType;
}

export const Badge: React.FC<BadgeProps> = ({ type }) => {
  const style = colorMap[type] ?? { background: "#f1f5f9", color: "#64748b" };
  const label = labelMap[type] ?? type;

  return (
    <span
      style={{
        ...style,
        borderRadius: 20,
        padding: "3px 12px",
        fontSize: 12,
        fontWeight: 600,
        display: "inline-block",
      }}
    >
      {label}
    </span>
  );
};
