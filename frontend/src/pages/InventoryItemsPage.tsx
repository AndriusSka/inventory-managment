import { useEffect, useState } from "react";
import { inventoryItemsApi } from "../api/inventoryItemsApi";
import { usersApi } from "../api/usersApi";
import type {
  InventoryItem,
  InventoryItemFilter,
} from "../types/InventoryItem";
import type { User } from "../types/User";
import { InventoryFilters } from "../components/InventoryFilters";
import { InventoryTable } from "../components/InventoryTable";
import { ExportButton } from "../components/ExportButton";

export function InventoryItemsPage() {
  const [items, setItems] = useState<InventoryItem[]>([]);
  const [users, setUsers] = useState<User[]>([]);
  const [filter, setFilter] = useState<InventoryItemFilter>({});
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  // Kraunami vartotojai
  useEffect(() => {
    async function loadUsers() {
      try {
        const data = await usersApi.getAll();
        setUsers(data);
      } catch (err) {
        console.error("Nepavyko gauti vartotojų:", err);
      }
    }
    loadUsers();
  }, []);

  // Kraunamas iventorius pagal filtrą
  useEffect(() => {
    async function loadItems() {
      setIsLoading(true);
      setError(null);
      try {
        const data = await inventoryItemsApi.getAll(filter);
        setItems(data);
      } catch (err) {
        setError("Nepavyko gauti inventoriaus.");
        console.error(err);
      } finally {
        setIsLoading(false);
      }
    }
    loadItems();
  }, [filter]);

  const handleDelete = async (id: string) => {
    try {
      await inventoryItemsApi.softDelete(id);
      // Po softdelete atnaujiname sąraša lokaliai, kad nereikėtų vėl kviesti API
      setItems((current) => current.filter((item) => item.id !== id));
    } catch (err) {
      setError("Nepavyko ištrinti įrašo.");
      console.error(err);
    }
  };

  return (
    <div>
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-xl font-semibold text-slate-800">Inventorius</h2>
        <ExportButton filter={filter} disabled={isLoading} />
      </div>

      <InventoryFilters filter={filter} users={users} onChange={setFilter} />

      {error && (
        <div className="bg-red-50 border border-red-200 rounded p-3 mb-4 text-sm text-red-700">
          {error}
        </div>
      )}

      {isLoading ? (
        <p className="text-slate-500">Kraunama...</p>
      ) : (
        <InventoryTable items={items} onDelete={handleDelete} />
      )}
    </div>
  );
}
