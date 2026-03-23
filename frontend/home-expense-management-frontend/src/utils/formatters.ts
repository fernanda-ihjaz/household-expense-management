export const formatCurrency = (value: number | undefined | null): string => {
  return `R$ ${(value || 0).toLocaleString("pt-BR", {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
  })}`;
};

export const generateUid = (): string => {
  return Math.random().toString(36).slice(2, 10);
};