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

    // Gaunamas filtras ir sablonas, pagal kuriuos generuojamas PDF failas
    public async Task<byte[]> ExportAsync(InventoryItemFilterDto filter, PdfTemplateType templateType)
    {
        var items = await _itemService.GetFilteredAsync(filter);
        var template = GetTemplate(templateType);
        return template.Generate(items);
    }

    private IPdfTemplate GetTemplate(PdfTemplateType templateType)
    {
        if (templateType == PdfTemplateType.Table)
        {
            return new TableTemplate();
        }

        if (templateType == PdfTemplateType.GroupedByUser)
        {
            return new GroupedByUserTemplate();
        }

        throw new ArgumentException("Nepalaikomas sablonas: " + templateType);
    }
}