import { useEffect, useMemo } from "react";
import { useAppContext } from "../context/AppContext";
import type { Transaction } from "../types";
import { transactionService } from "../services/transactionService";

export const useTransactions = () => {
  
const { 
  transactions = [], 
  setTransactions, 
  people = [], 
  categories = [] 
} = useAppContext();

  useEffect(() => {
    const loadTransactions = async () => {
      try {
        const data = await transactionService.getAll();
        setTransactions(data);
      } catch (error) {
        console.error("Erro ao carregar transações", error);
      }
    };

    loadTransactions();
  }, []);

  const addTransaction = async (data: Omit<Transaction, "id">) => {
    try {
      const created = await transactionService.create(data);
      setTransactions((prev) => [...prev, created]);
    } catch (error) {
      console.error("Erro ao criar transação", error);
    }
  };

const totalIncome = useMemo(() => {
  return (transactions ?? [])
    .filter((t) => t.type === "Income")
    .reduce((acc, t) => acc + t.amount, 0);
}, [transactions]);

const totalExpense = useMemo(() => {
  return (transactions ?? [])
    .filter((t) => t.type === "Expense")
    .reduce((acc, t) => acc + t.amount, 0);
}, [transactions]);

  const netBalance = totalIncome - totalExpense;

  const summaryByPerson = useMemo(() => {
    return (people ?? []).map((p) => {
      const ts = (transactions ?? []).filter((t) => t.personId === p.id);

      const income = ts
        .filter((t) => t.type === "Income")
        .reduce((acc, t) => acc + t.amount, 0);

      const expense = ts
        .filter((t) => t.type === "Expense")
        .reduce((acc, t) => acc + t.amount, 0);

      return {
        id: p.id,
        name: p.name,
        subtext: `${p.age} anos`,
        income,
        expense,
        balance: income - expense,
      };
    });
  }, [people, transactions]);

  const summaryByCategory = useMemo(() => {
    return categories.map((c) => {
      const ts = transactions.filter((t) => t.categoryId === c.id);

      const income = ts
        .filter((t) => t.type === "Income")
        .reduce((acc, t) => acc + t.amount, 0);

      const expense = ts
        .filter((t) => t.type === "Expense")
        .reduce((acc, t) => acc + t.amount, 0);

      return {
        id: c.id,
        name: c.description,
        subtext: c.purpose,
        income,
        expense,
        balance: income - expense,
      };
    });
  }, [categories, transactions]);

  return {
    transactions,
    addTransaction,
    totalIncome,
    totalExpense,
    netBalance,
    summaryByPerson,
    summaryByCategory,
  };
};