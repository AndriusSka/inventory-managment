using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Enums;

namespace InventoryManagement.Core.Interfaces;

public interface IPdfExportService
{
    Task<byte[]> ExportAsync(InventoryItemFilterDto filter, PdfTemplateType templateType);
}