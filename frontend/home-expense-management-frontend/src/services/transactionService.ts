import { api } from "./api";
import type { Transaction } from "../types";

interface TransactionModel {
  description: string;
  amount:      number;
  type:        number;
  personId:    string;
  categoryId:  string;
}

interface TransactionCreatedResponse {
  id: string;
}

const typeStringToId: Record<string, number> = {
  Expense: 1,
  Income:  2,
};

const typeIdToString: Record<number, string> = {
  1: "Expense",
  2: "Income",
};

const mapTransaction = (raw: any): Transaction => ({
  id:          raw.id,
  description: raw.description,
  amount:      raw.amount,
  personId:    raw.personId,
  categoryId:  raw.categoryId,
  type:        raw.type ?? typeIdToString[raw.purpose] ?? "Expense",
  typeId:      raw.typeId ?? typeStringToId[raw.purpose] ?? 1,
});

export const transactionService = {
  getAllByPersons: async (personId?: string): Promise<Transaction[]> => {
    if (!personId) return [];
    const data = await api<any[]>(`/api/transactions/person/${personId}`);
    return data.map(mapTransaction);
  },

  getById: async (id: string): Promise<Transaction> => {
    const data = await api<any>(`/api/transactions/${id}`);
    return mapTransaction(data);
  },

  getAll: async (): Promise<Transaction[]> => {
    const data = await api<any>(`/api/transactions`);
    return data.map(mapTransaction);
  },

  create: async (transaction: Omit<Transaction, "id">): Promise<Transaction> => {
    const body: TransactionModel = {
      description: transaction.description,
      amount:      transaction.amount,
      personId:    transaction.personId,
      categoryId:  transaction.categoryId,
      type:
        typeof transaction.typeId === "number"
          ? transaction.typeId
          : typeStringToId[transaction.type ?? "Expense"] ?? 1,
    };

    const response = await api<TransactionCreatedResponse>("/api/transactions", {
      method: "POST",
      body: JSON.stringify(body),
    });

    return transactionService.getById(response.id);
  },

  delete: (id: string): Promise<void> => {
    return api<void>(`/api/transactions/${id}`, {
      method: "DELETE",
    });
  },
};