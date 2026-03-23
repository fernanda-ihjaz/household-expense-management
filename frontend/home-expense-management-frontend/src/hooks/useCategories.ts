import { useEffect } from "react";
import { useAppContext } from "../context/AppContext";
import type { Category } from "../types";
import { categoryService } from "../services/categoryService";

export const useCategories = () => {
  const { categories, setCategories } = useAppContext();

  useEffect(() => {
    const loadCategories = async () => {
      try {
        const data = await categoryService.getAll();
        setCategories(data);
      } catch (error) {
        console.error("Erro ao carregar categorias", error);
      }
    };

    loadCategories();
  }, []);

  const addCategory = async (data: Omit<Category, "id">) => {
    try {
      const newCategory = await categoryService.create(data);
      setCategories((prev) => [...prev, newCategory]);
    } catch (error) {
      console.error("Erro ao criar categoria", error);
    }
  };

  const deleteCategory = async (id: string) => {
    try {
      await categoryService.delete(id);
      setCategories((prev) => prev.filter((c) => c.id !== id));
    } catch (error) {
      console.error("Erro ao deletar categoria", error);
    }
  };

  return {
    categories,
    addCategory,
    deleteCategory,
  };
};