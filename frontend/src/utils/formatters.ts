import type { ItemType } from "../types/InventoryItem";

const itemTypeLabels: Record<ItemType, string> = {
  Tablet: "Planšetė",
  Phone: "Telefonas",
  SimCard: "SIM kortelė",
  Laptop: "Nešiojamas",
};

export function formatItemType(type: ItemType): string {
  return itemTypeLabels[type];
}

export function formatDate(isoDate: string): string {
  const date = new Date(isoDate);
  return date.toLocaleDateString("lt-LT");
}
