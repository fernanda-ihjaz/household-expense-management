export const TransactionTypeId = {
  Expense: 1,
  Income:  2,
} as const;

export type TransactionTypeId = typeof TransactionTypeId[keyof typeof TransactionTypeId];

export const CategoryPurposeId = {
  Expense: 1,
  Income:  2,
  Both:    3,
} as const;

export type CategoryPurposeId = typeof CategoryPurposeId[keyof typeof CategoryPurposeId];

export type TransactionType = 'Income' | 'Expense';
export type PurposeType     = 'Income' | 'Expense' | 'Both';

export const purposeIdToString: Record<CategoryPurposeId, PurposeType> = {
  [CategoryPurposeId.Expense]: 'Expense',
  [CategoryPurposeId.Income]:  'Income',
  [CategoryPurposeId.Both]:    'Both',
};

export const purposeStringToId: Record<PurposeType, CategoryPurposeId> = {
  Expense: CategoryPurposeId.Expense,
  Income:  CategoryPurposeId.Income,
  Both:    CategoryPurposeId.Both,
};

export const typeIdToString: Record<TransactionTypeId, TransactionType> = {
  [TransactionTypeId.Expense]: 'Expense',
  [TransactionTypeId.Income]:  'Income',
};

export const typeStringToId: Record<TransactionType, TransactionTypeId> = {
  Expense: TransactionTypeId.Expense,
  Income:  TransactionTypeId.Income,
};

export const transactionTypeLabel: Record<TransactionType, string> = {
  Income:  'Receita',
  Expense: 'Despesa',
};

export const purposeLabel: Record<PurposeType, string> = {
  Income:  'Receita',
  Expense: 'Despesa',
  Both:    'Ambos',
};

export interface Person {
  id:   string;
  name: string;
  age:  number;
}

export interface Category {
  id:          string;
  description: string;
  purpose:     PurposeType;
  purposeId:   CategoryPurposeId;
}

export interface Transaction {
  id:          string;
  description: string;
  amount:      number;
  type:        TransactionType;
  typeId:      TransactionTypeId;
  categoryId:  string;
  personId:    string;
}
