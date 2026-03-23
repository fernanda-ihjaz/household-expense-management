import type { ReactNode } from "react";

interface ModalProps {
  title: string;
  onClose: () => void;
  children: ReactNode;
}

export const Modal: React.FC<ModalProps> = ({ title, onClose, children }) => (
  <div
    style={{
      position: "fixed",
      inset: 0,
      background: "rgba(0,0,0,0.35)",
      zIndex: 1000,
      display: "flex",
      alignItems: "center",
      justifyContent: "center",
    }}
  >
    <div
      style={{
        background: "#fff",
        borderRadius: 16,
        padding: "28px 32px",
        minWidth: 360,
        maxWidth: 480,
        width: "90%",
        boxShadow: "0 20px 60px rgba(0,0,0,.15)",
      }}
    >
      <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center", marginBottom: 20 }}>
        <h3 style={{ margin: 0, fontSize: 18, fontWeight: 700, color: "#1e293b" }}>
          {title}
        </h3>
        <button
          onClick={onClose}
          style={{
            border: "none",
            background: "none",
            cursor: "pointer",
            fontSize: 20,
            color: "#9ca3af",
            lineHeight: 1,
          }}
        >
          ×
        </button>
      </div>
      {children}
    </div>
  </div>
);