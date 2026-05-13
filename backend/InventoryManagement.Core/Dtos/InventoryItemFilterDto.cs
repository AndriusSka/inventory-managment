using InventoryManagement.Core.Enums;

namespace InventoryManagement.Core.Dtos;

public class InventoryItemFilterDto
{
    public ItemType? Type { get; set; }
    public string? Comment { get; set; }
    public Guid? UserId { get; set; }
}