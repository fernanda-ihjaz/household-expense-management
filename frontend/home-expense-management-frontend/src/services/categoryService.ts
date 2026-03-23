import { api } from "./api";
import { type Category } from "../types/";
import { type PurposeType } from "../types/";


interface CategoryModel {
  description: string;
  purposeId: number;
}

const purposeIdToString: Record<number, PurposeType> = {
  1: "Expense",
  2: "Income",
  3: "Both",
};

const purposeStringToId: Record<PurposeType, number> = {
  Expense: 1,
  Income: 2,
  Both: 3,
};

const mapCategory = (raw: any): Category => {
  const purposeId = raw.purpose ?? 3;

  return {
    id: raw.id,
    description: raw.description,
    purposeId,
    purpose: purposeIdToString[purposeId] ?? "Both",
  };
};

export const categoryService = {
  getAll: async (): Promise<Category[]> => {
    const data = await api<any[]>("/api/categories");
    return data.map(mapCategory);
  },

  create: async (category: Omit<Category, "id">): Promise<Category> => {

    const body: CategoryModel = {
      description: category.description,
      purposeId: purposeStringToId[category.purpose],
    };
    
    await api<void>("/api/categories", {
      method: "POST",
      body: JSON.stringify(body),
    });

    const all = await categoryService.getAll();
    const created = all.find((c) => c.description === category.description);

    if (!created) throw new Error("Category created but could not be retrieved.");
    return created;
  },

  delete: (_id: string): Promise<void> => {
    throw new Error("Category deletion is not supported by the API.");
  },
};