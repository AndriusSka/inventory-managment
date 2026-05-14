import { apiClient } from "./apiClient";
import type {
  InventoryItem,
  InventoryItemFilter,
} from "../types/InventoryItem";

export const inventoryItemsApi = {
  async getAll(filter: InventoryItemFilter = {}): Promise<InventoryItem[]> {
    const response = await apiClient.get<InventoryItem[]>("/inventoryitems", {
      params: filter,
    });
    return response.data;
  },

  async softDelete(id: string): Promise<void> {
    await apiClient.delete(`/inventoryitems/${id}`);
  },
};
