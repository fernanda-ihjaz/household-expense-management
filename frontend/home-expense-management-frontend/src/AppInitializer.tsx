import { useCategories } from "./hooks/useCategories";
import { usePeople } from "./hooks/usePeople";
import { useTransactions } from "./hooks/useTransactions";

export const AppInitializer = () => {
  useCategories();
  usePeople();
  useTransactions();
  return null;
};
