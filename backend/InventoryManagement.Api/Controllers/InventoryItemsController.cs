using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Enums;
using InventoryManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryItemsController : ControllerBase
{
    private readonly IInventoryItemService _itemService;

    public InventoryItemsController(IInventoryItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAll(
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

        var items = await _itemService.GetFilteredAsync(filter);
        return Ok(items);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        var success = await _itemService.SoftDeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}