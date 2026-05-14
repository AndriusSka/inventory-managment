import type { InventoryItem } from "../types/InventoryItem";
import { formatDate, formatItemType } from "../utils/formatters";

interface InventoryTableProps {
  items: InventoryItem[];
  onDelete: (id: string) => void;
}

export function InventoryTable({ items, onDelete }: InventoryTableProps) {
  if (items.length === 0) {
    return (
      <div className="bg-white rounded-lg shadow-sm border p-8 text-center">
        <p className="text-slate-500">
          Nerasta įrašų pagal pasirinktus filtrus.
        </p>
      </div>
    );
  }

  const handleDeleteClick = (item: InventoryItem) => {
    const confirmed = window.confirm(
      `Ar tikrai norite ištrinti šį įrašą?\n\n${formatItemType(item.type)} — ${item.comment}`,
    );

    if (confirmed) {
      onDelete(item.id);
    }
  };

  return (
    <div className="bg-white rounded-lg shadow-sm border overflow-hidden">
      <table className="w-full">
        <thead className="bg-slate-100">
          <tr>
            <th className="px-4 py-3 text-left text-sm font-medium text-slate-700">
              Tipas
            </th>
            <th className="px-4 py-3 text-left text-sm font-medium text-slate-700">
              Komentaras
            </th>
            <th className="px-4 py-3 text-left text-sm font-medium text-slate-700">
              Vartotojas
            </th>
            <th className="px-4 py-3 text-left text-sm font-medium text-slate-700">
              Pirkimo data
            </th>
            <th className="px-4 py-3 text-right text-sm font-medium text-slate-700">
              Veiksmai
            </th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => (
            <tr key={item.id} className="border-t hover:bg-slate-50">
              <td className="px-4 py-3 text-sm text-slate-800">
                {formatItemType(item.type)}
              </td>
              <td className="px-4 py-3 text-sm text-slate-800">
                {item.comment}
              </td>
              <td className="px-4 py-3 text-sm text-slate-800">
                {item.userFullName}
              </td>
              <td className="px-4 py-3 text-sm text-slate-600">
                {formatDate(item.purchaseDate)}
              </td>
              <td className="px-4 py-3 text-right">
                <button
                  onClick={() => handleDeleteClick(item)}
                  className="px-3 py-1 text-sm text-red-600 hover:bg-red-50 rounded border border-red-200"
                >
                  Ištrinti
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
