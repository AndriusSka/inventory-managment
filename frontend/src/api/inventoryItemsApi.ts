import { apiClient } from "./apiClient";
import type {
  InventoryItem,
  InventoryItemFilter,
} from "../types/InventoryItem";
import type { PdfTemplateType } from "../types/PdfExport";

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

  async exportPdf(
    filter: InventoryItemFilter,
    template: PdfTemplateType,
  ): Promise<Blob> {
    const response = await apiClient.get("/export/pdf", {
      params: { ...filter, template },
      // blob responseType, kad gautume PDF failą kaip Blob objektą
      // Blob (Binary Large Object), nes PDF yra binarinis failas, o ne tekstas
      responseType: "blob",
    });
    return response.data;
  },
};
