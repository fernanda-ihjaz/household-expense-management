import type { ReactNode } from "react";

interface EmptyStateProps {
  icon: string | ReactNode;
  message: string;
  hint?: string;
}

export const EmptyState: React.FC<EmptyStateProps> = ({ icon, message, hint }) => (
  <div style={{ display: "flex", flexDirection: "column", alignItems: "center", padding: "48px 0", color: "#9ca3af" }}>
    <div style={{ fontSize: 40, marginBottom: 12 }}>{icon}</div>
    <div style={{ fontWeight: 600, fontSize: 15, color: "#6b7280" }}>{message}</div>
    {hint && <div style={{ fontSize: 13, marginTop: 4 }}>{hint}</div>}
  </div>
);