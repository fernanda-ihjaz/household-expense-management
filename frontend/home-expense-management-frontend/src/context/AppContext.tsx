import React, { createContext, useState, type ReactNode, useContext } from 'react';
import type { Person, Category, Transaction } from '../types';

interface AppContextData {
  people: Person[];
  setPeople: React.Dispatch<React.SetStateAction<Person[]>>;
  categories: Category[];
  setCategories: React.Dispatch<React.SetStateAction<Category[]>>;
  transactions: Transaction[];
  setTransactions: React.Dispatch<React.SetStateAction<Transaction[]>>;
}

const AppContext = createContext<AppContextData>({} as AppContextData);

export const AppProvider = ({ children }: { children: ReactNode }) => {
  const [people, setPeople] = useState<Person[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  return (
    <AppContext.Provider value={{ people, setPeople, categories, setCategories, transactions, setTransactions }}>
      {children}
    </AppContext.Provider>
  );
};

export const useAppContext = () => useContext(AppContext);