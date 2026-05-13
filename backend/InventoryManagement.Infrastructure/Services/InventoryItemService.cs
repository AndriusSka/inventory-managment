using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Interfaces;
using InventoryManagement.Core.Mappers;

namespace InventoryManagement.Infrastructure.Services;

public class InventoryItemService : IInventoryItemService
{
    private readonly IInventoryItemRepository _repository;

    public InventoryItemService(IInventoryItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<InventoryItemDto>> GetFilteredAsync(InventoryItemFilterDto filter)
    {
        var items = await _repository.GetAllAsync(filter.Type, filter.Comment, filter.UserId);
        return items.Select(i => i.ToDto());
    }

    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
            return false;

        await _repository.SoftDeleteAsync(id);
        return true;
    }
}