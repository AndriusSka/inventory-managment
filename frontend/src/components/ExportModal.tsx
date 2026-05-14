import { useState } from "react";
import type { PdfTemplateType } from "../types/PdfExport";

interface ExportModalProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: (template: PdfTemplateType) => void;
  isExporting: boolean;
}

const templateOptions: {
  value: PdfTemplateType;
  label: string;
  description: string;
}[] = [
  {
    value: "Table",
    label: "Lentelė",
    description: "Visi įrašai vienoje suvestinėje lentelėje",
  },
  {
    value: "GroupedByUser",
    label: "Sugrupuota pagal vartotojus",
    description: "Įrašai sugrupuoti į blokus pagal kiekvieną vartotoją",
  },
];

export function ExportModal({
  isOpen,
  onClose,
  onConfirm,
  isExporting,
}: ExportModalProps) {
  const [selected, setSelected] = useState<PdfTemplateType>("Table");

  if (!isOpen) return null;

  return (
    <div
      className="fixed inset-0 bg-black/40 flex items-center justify-center z-50 p-4"
      onClick={onClose}
    >
      <div
        className="bg-white rounded-lg shadow-lg max-w-md w-full"
        onClick={(e) => e.stopPropagation()}
      >
        <div className="p-6 border-b">
          <h3 className="text-lg font-semibold text-slate-800">
            Eksportuoti į PDF
          </h3>
          <p className="text-sm text-slate-500 mt-1">
            Pasirinkite dokumento šabloną
          </p>
        </div>

        <div className="p-6 space-y-3">
          {templateOptions.map((option) => (
            <label
              key={option.value}
              className={`flex items-start gap-3 p-3 border rounded cursor-pointer transition ${
                selected === option.value
                  ? "border-blue-500 bg-blue-50"
                  : "border-slate-200 hover:bg-slate-50"
              }`}
            >
              <input
                type="radio"
                name="template"
                value={option.value}
                checked={selected === option.value}
                onChange={() => setSelected(option.value)}
                className="mt-1"
              />
              <div>
                <div className="font-medium text-slate-800">{option.label}</div>
                <div className="text-sm text-slate-500">
                  {option.description}
                </div>
              </div>
            </label>
          ))}
        </div>

        <div className="p-6 border-t flex justify-end gap-2">
          <button
            onClick={onClose}
            disabled={isExporting}
            className="px-4 py-2 text-sm text-slate-700 hover:bg-slate-100 rounded border disabled:opacity-50"
          >
            Atšaukti
          </button>
          <button
            onClick={() => onConfirm(selected)}
            disabled={isExporting}
            className="px-4 py-2 text-sm bg-blue-600 text-white hover:bg-blue-700 rounded disabled:opacity-50"
          >
            {isExporting ? "Eksportuojama..." : "Eksportuoti"}
          </button>
        </div>
      </div>
    </div>
  );
}
