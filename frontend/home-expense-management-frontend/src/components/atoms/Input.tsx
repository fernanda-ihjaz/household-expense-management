import React from "react";

interface InputProps {
  label?: string;
  value: string | number;
  onChange: (value: string) => void;
  type?: React.HTMLInputTypeAttribute;
  placeholder?: string;
  maxLength?: number;
  required?: boolean;
  error?: string;
}

export const Input: React.FC<InputProps> = ({
  label,
  value,
  onChange,
  type = "text",
  placeholder,
  maxLength,
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
    <input
      type={type}
      value={value}
      onChange={(e) => onChange(e.target.value)}
      placeholder={placeholder}
      maxLength={maxLength}
      style={{
        border: `1.5px solid ${error ? "#ef4444" : "#e2e8f0"}`,
        borderRadius: 8,
        padding: "9px 13px",
        fontSize: 14,
        fontFamily: "inherit",
        outline: "none",
        transition: "border .15s",
        background: "#fff",
        color: "#1e293b",
      }}
    />
    {error && <span style={{ fontSize: 12, color: "#ef4444" }}>{error}</span>}
  </div>
);