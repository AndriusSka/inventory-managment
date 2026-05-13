using InventoryManagement.Core.Dtos;

namespace InventoryManagement.Core.Interfaces;

public interface IInventoryItemService
{
    Task<IEnumerable<InventoryItemDto>> GetFilteredAsync(InventoryItemFilterDto filter);
    Task<bool> SoftDeleteAsync(Guid id);
}