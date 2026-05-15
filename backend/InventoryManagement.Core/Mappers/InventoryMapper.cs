using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Entities;

namespace InventoryManagement.Core.Mappers;

public static class InventoryMappers
{
    public static UserDto MapUserToDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public static InventoryItemDto MapItemToDto(InventoryItem item)
    {
        var dto = new InventoryItemDto();
        dto.Id = item.Id;
        dto.Type = item.Type.ToString();
        dto.Comment = item.Comment;
        dto.PurchaseDate = item.PurchaseDate;
        dto.UserId = item.UserId;
        dto.UserFullName = GetUserFullName(item.User);
        return dto;
    }

    private static string GetUserFullName(User user)
    {
        if (user == null)
        {
            return string.Empty;
        }

        return user.FirstName + " " + user.LastName;
    }
}