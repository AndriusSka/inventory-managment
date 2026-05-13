using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Enums;
using InventoryManagement.Core.Interfaces;
using InventoryManagement.Infrastructure.Services.PdfTemplates;

namespace InventoryManagement.Infrastructure.Services;

public class PdfExportService : IPdfExportService
{
    private readonly IInventoryItemService _itemService;

    public PdfExportService(IInventoryItemService itemService)
    {
        _itemService = itemService;
    }

    public async Task<byte[]> ExportAsync(InventoryItemFilterDto filter, PdfTemplateType templateType)
    {
        // Gaunami duomenys
        var items = await _itemService.GetFilteredAsync(filter);

        // Pasirenkamas šablonas
        IPdfTemplate template = templateType switch
        {
            PdfTemplateType.Table => new TableTemplate(),
            PdfTemplateType.GroupedByUser => new GroupedByUserTemplate(),
            _ => throw new ArgumentException($"Nepalaikomas šablonas: {templateType}")
        };

        return template.Generate(items);
    }
}