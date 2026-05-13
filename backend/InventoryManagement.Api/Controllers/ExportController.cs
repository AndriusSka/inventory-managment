using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Enums;
using InventoryManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExportController : ControllerBase
{
    private readonly IPdfExportService _exportService;

    public ExportController(IPdfExportService exportService)
    {
        _exportService = exportService;
    }

    [HttpGet("pdf")]
    public async Task<IActionResult> ExportPdf(
        [FromQuery] PdfTemplateType template,
        [FromQuery] ItemType? type,
        [FromQuery] string? comment,
        [FromQuery] Guid? userId)
    {
        var filter = new InventoryItemFilterDto
        {
            Type = type,
            Comment = comment,
            UserId = userId
        };

        var pdfBytes = await _exportService.ExportAsync(filter, template);

        var fileName = $"report-{DateTime.Now:yyyyMMdd}.pdf";
        return File(pdfBytes, "application/pdf", fileName);
    }
}