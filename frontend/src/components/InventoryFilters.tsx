import type { InventoryItemFilter, ItemType } from "../types/InventoryItem";
import type { User } from "../types/User";
import { formatItemType } from "../utils/formatters";

interface InventoryFiltersProps {
  filter: InventoryItemFilter;
  users: User[];
  onChange: (filter: InventoryItemFilter) => void;
}

const itemTypes: ItemType[] = ["Tablet", "Phone", "SimCard", "Laptop"];

export function InventoryFilters({
  filter,
  users,
  onChange,
}: InventoryFiltersProps) {
  const handleTypeChange = (value: string) => {
    const newFilter = { ...filter };

    if (value === "") {
      newFilter.type = undefined;
    } else {
      newFilter.type = value as ItemType;
    }

    onChange(newFilter);
  };

  const handleCommentChange = (value: string) => {
    const newFilter = { ...filter };

    if (value === "") {
      newFilter.comment = undefined;
    } else {
      newFilter.comment = value;
    }

    onChange(newFilter);
  };

  const handleUserChange = (value: string) => {
    const newFilter = { ...filter };

    if (value === "") {
      newFilter.userId = undefined;
    } else {
      newFilter.userId = value;
    }

    onChange(newFilter);
  };

  const handleClear = () => {
    onChange({});
  };

  return (
    <div className="bg-white rounded-lg shadow-sm border p-4 mb-4">
      <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
        <div>
          <label className="block text-sm font-medium text-slate-700 mb-1">
            Tipas
          </label>
          <select
            value={filter.type ?? ""}
            onChange={(e) => handleTypeChange(e.target.value)}
            className="w-full border rounded px-3 py-2 text-sm"
          >
            <option value="">Visi tipai</option>
            {itemTypes.map((type) => (
              <option key={type} value={type}>
                {formatItemType(type)}
              </option>
            ))}
          </select>
        </div>

        <div>
          <label className="block text-sm font-medium text-slate-700 mb-1">
            Komentaras
          </label>
          <input
            type="text"
            value={filter.comment ?? ""}
            onChange={(e) => handleCommentChange(e.target.value)}
            placeholder="Ieškoti..."
            className="w-full border rounded px-3 py-2 text-sm"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-slate-700 mb-1">
            Vartotojas
          </label>
          <select
            value={filter.userId ?? ""}
            onChange={(e) => handleUserChange(e.target.value)}
            className="w-full border rounded px-3 py-2 text-sm"
          >
            <option value="">Visi vartotojai</option>
            {users.map((user) => (
              <option key={user.id} value={user.id}>
                {user.firstName} {user.lastName}
              </option>
            ))}
          </select>
        </div>

        <div className="flex items-end">
          <button
            onClick={handleClear}
            className="px-4 py-2 text-sm text-slate-600 hover:bg-slate-100 rounded border"
          >
            Išvalyti filtrus
          </button>
        </div>
      </div>
    </div>
  );
}
