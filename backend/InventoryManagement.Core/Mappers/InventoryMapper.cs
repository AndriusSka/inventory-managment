using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Entities;

namespace InventoryManagement.Core.Mappers;

public static class InventoryMappers
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public static InventoryItemDto ToDto(this InventoryItem item)
    {
        return new InventoryItemDto
        {
        Id = item.Id,
        // Konvertuojama enum reikšmė į string
        Type = item.Type.ToString(),
        Comment = item.Comment,
        PurchaseDate = item.PurchaseDate,
        UserId = item.UserId,
        UserFullName = item.User != null
            ? $"{item.User.FirstName} {item.User.LastName}"
            : string.Empty
        };
    }
}