import { useState } from "react";
import { inventoryItemsApi } from "../api/inventoryItemsApi";
import type { InventoryItemFilter } from "../types/InventoryItem";
import type { PdfTemplateType } from "../types/PdfExport";
import { downloadBlob } from "../utils/downloadFile";
import { ExportModal } from "./ExportModal";

interface ExportButtonProps {
  filter: InventoryItemFilter;
  disabled?: boolean;
}

export function ExportButton({ filter, disabled }: ExportButtonProps) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isExporting, setIsExporting] = useState(false);

  const handleExport = async (template: PdfTemplateType) => {
    setIsExporting(true);
    try {
      const blob = await inventoryItemsApi.exportPdf(filter, template);
      // Generuojame failo pavadinimą su timestamp, rodant tik datą be tikslaus laiko
      const timestamp = new Date()
        .toISOString()
        .replace(/[:.]/g, "-")
        .slice(0, 10);
      const fileName = `inventorius-${timestamp}.pdf`;

      downloadBlob(blob, fileName);
      setIsModalOpen(false);
    } catch (err) {
      console.error("PDF eksporto klaida:", err);
      alert("Nepavyko eksportuoti PDF. Bandykite dar kartą.");
    } finally {
      setIsExporting(false);
    }
  };

  return (
    <>
      <button
        onClick={() => setIsModalOpen(true)}
        disabled={disabled}
        className="px-4 py-2 text-sm bg-blue-600 text-white hover:bg-blue-700 rounded disabled:opacity-50 disabled:cursor-not-allowed"
      >
        Eksportuoti į PDF
      </button>

      <ExportModal
        isOpen={isModalOpen}
        // onClose funkcija uždaro modalą, jei nėra vykdomas eksportas
        onClose={() => !isExporting && setIsModalOpen(false)}
        onConfirm={handleExport}
        isExporting={isExporting}
      />
    </>
  );
}
