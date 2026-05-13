using InventoryManagement.Core.Dtos;

namespace InventoryManagement.Core.Interfaces;

public interface IPdfTemplate
{
    byte[] Generate(IEnumerable<InventoryItemDto> items);
}