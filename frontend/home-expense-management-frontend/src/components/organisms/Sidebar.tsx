import React from 'react';

const NAV = [
  { id: "dashboard", label: "Início", icon: "" },
  { id: "people", label: "Pessoas", icon: "" },
  { id: "categories", label: "Categorias", icon: "" },
  { id: "transactions", label: "Transações", icon: "" },
];

interface SidebarProps {
  currentPage: string;
  setPage: (page: string) => void;
}

export const Sidebar: React.FC<SidebarProps> = ({ currentPage, setPage }) => {
  return (
    <aside style={{
      width: 200, background: "#fff", borderRight: "1.5px solid #e2e8f0",
      display: "flex", flexDirection: "column", padding: "24px 0", position: "fixed", top: 0, left: 0, bottom: 0,
    }}>
      <div style={{ padding: "0 20px 24px", borderBottom: "1px solid #f1f5f9" }}>
        <div style={{ display: "flex", alignItems: "center", gap: 10 }}>
          <div>
            <div style={{ fontWeight: 800, fontSize: 18, color: "#0f172a", lineHeight: 1.2 }}>Controle de Gastos</div>
            <div style={{ fontSize: 16, color: "#94a3b8" }}>Residencial</div>
          </div>
        </div>
      </div>

      <nav style={{ padding: "16px 12px", flex: 1 }}>
        {NAV.map(n => (
          <button key={n.id} onClick={() => setPage(n.id)} style={{
            width: "100%", textAlign: "left", border: "none", borderRadius: 8, padding: "10px 12px",
            cursor: "pointer", fontFamily: "inherit", fontWeight: 600, fontSize: 14,
            display: "flex", alignItems: "center", gap: 10, marginBottom: 4, transition: "all .15s",
            background: currentPage === n.id ? "#eff6ff" : "transparent",
            color: currentPage === n.id ? "#2563eb" : "#64748b",
          }}>
            <span style={{ fontSize: 16 }}>{n.icon}</span>
            {n.label}
          </button>
        ))}
      </nav>
    </aside>
  );
};