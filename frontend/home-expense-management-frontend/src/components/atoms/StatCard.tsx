import React from "react";
import { formatCurrency } from "../../utils/helpers";

export type StatCardType = "Income" | "Expense" | "Balance";

interface StatCardProps {
  label: string;
  value: number;
  type: StatCardType;
}

export const StatCard: React.FC<StatCardProps> = ({ label, value, type }) => {
  const colors: Record<StatCardType, { bg: string; border: string; val: string }> = {
    Income: { bg: "#f0fdf4", border: "#bbf7d0", val: "#16a34a" },
    Expense: { bg: "#fef2f2", border: "#fecaca", val: "#dc2626" },
    Balance: { bg: "#eff6ff", border: "#bfdbfe", val: "#2563eb" },
  };
  
  const c = colors[type] || colors.Balance;
  
  return (
    <div style={{ background: c.bg, border: `1.5px solid ${c.border}`, borderRadius: 12, padding: "16px 20px", minWidth: 160, flex: 1 }}>
      <div style={{ fontSize: 12, color: "#6b7280", fontWeight: 500, marginBottom: 6 }}>
        {label}
      </div>
      <div style={{ fontSize: 22, fontWeight: 700, color: c.val }}>
        {formatCurrency(value)}
      </div>
    </div>
  );
};