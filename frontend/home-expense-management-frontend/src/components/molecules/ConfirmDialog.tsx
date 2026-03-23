import React from "react";
import { Modal } from "./Modal";
import { Button } from "../atoms/Button";

interface ConfirmDialogProps {
  message: string;
  onConfirm: () => void;
  onCancel: () => void;
}

export const ConfirmDialog: React.FC<ConfirmDialogProps> = ({
  message,
  onConfirm,
  onCancel,
}) => (
  <Modal title="Confirmar Exclusão" onClose={onCancel}>
    <p style={{ color: "#374151", marginBottom: 20 }}>{message}</p>
    <div style={{ display: "flex", gap: 10, justifyContent: "flex-end" }}>
      <Button variant="ghost" onClick={onCancel}>
        Cancelar
      </Button>
      <Button variant="danger" onClick={onConfirm}>
        Excluir
      </Button>
    </div>
  </Modal>
);