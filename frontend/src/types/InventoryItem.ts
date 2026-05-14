export type ItemType = "Tablet" | "Phone" | "SimCard" | "Laptop";

export interface InventoryItem {
  id: string;
  type: ItemType;
  comment: string;
  purchaseDate: string;
  userId: string;
  userFullName: string;
}

export interface InventoryItemFilter {
  type?: ItemType;
  comment?: string;
  userId?: string;
}
