import React from 'react';

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: "primary" | "ghost" | "danger" | "success";
  small?: boolean;
}

export const Button: React.FC<ButtonProps> = ({ children, variant = "primary", disabled, small, ...props }) => {
  const base = {
    border: "none", borderRadius: 8, cursor: disabled ? "not-allowed" : "pointer",
    fontFamily: "inherit", fontWeight: 600, transition: "all .15s",
    padding: small ? "6px 14px" : "10px 20px", fontSize: small ? 13 : 14,
    opacity: disabled ? 0.5 : 1,
  };
  const styles = {
    primary: { background: "#2563eb", color: "#fff" },
    ghost: { background: "transparent", color: "#64748b", border: "1px solid #e2e8f0" },
    danger: { background: "#fee2e2", color: "#dc2626" },
    success: { background: "#dcfce7", color: "#16a34a" },
  };

  return (
    <button style={{ ...base, ...styles[variant] }} disabled={disabled} {...props}>
      {children}
    </button>
  );
};