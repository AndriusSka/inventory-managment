using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;

namespace InventoryManagement.Core.Interfaces;

public interface IInventoryItemRepository
{
    Task<IEnumerable<InventoryItem>> GetAllAsync(ItemType? type, string? comment, Guid? userId);
    Task<InventoryItem?> GetByIdAsync(Guid id);
    Task SoftDeleteAsync(Guid id);
}