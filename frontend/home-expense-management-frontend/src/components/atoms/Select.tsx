import React from "react";

export interface SelectOption {
  value: string;
  label: string;
  disabled?: boolean;
}

interface SelectProps {
  label?: string;
  value: string;
  onChange: (value: string) => void;
  options: SelectOption[];
  placeholder?: string;
  required?: boolean;
  error?: string;
}

export const Select: React.FC<SelectProps> = ({
  label,
  value,
  onChange,
  options,
  placeholder,
  required,
  error,
}) => (
  <div style={{ display: "flex", flexDirection: "column", gap: 4 }}>
    {label && (
      <label style={{ fontSize: 13, fontWeight: 600, color: "#374151" }}>
        {label}
        {required && " *"}
      </label>
    )}
    <select
      value={value}
      onChange={(e) => onChange(e.target.value)}
      style={{
        border: `1.5px solid ${error ? "#ef4444" : "#e2e8f0"}`,
        borderRadius: 8,
        padding: "9px 13px",
        fontSize: 14,
        fontFamily: "inherit",
        outline: "none",
        background: "#fff",
        color: value ? "#1e293b" : "#9ca3af",
      }}
    >
      <option value="">{placeholder || "Select..."}</option>
      {options.map((o) => (
        <option key={o.value} value={o.value} disabled={o.disabled}>
          {o.label}
        </option>
      ))}
    </select>
    {error && <span style={{ fontSize: 12, color: "#ef4444" }}>{error}</span>}
  </div>
);