using FluentAssertions;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
using InventoryManagement.Core.Mappers;
using Xunit;

namespace InventoryManagement.Tests.Mappers;

public class InventoryMappersTests
{
    [Fact]
    public void MapUserToDto_MapsAllFields()
    {
        // Paruošimas
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Jonas",
            LastName = "Petraitis"
        };

        // Vykdymas
        var dto = InventoryMappers.MapUserToDto(user);

        // Tikrinimas
        dto.Id.Should().Be(user.Id);
        dto.FirstName.Should().Be("Jonas");
        dto.LastName.Should().Be("Petraitis");
    }

    [Fact]
    public void MapItemToDto_ConvertsEnumToString()
    {
        var item = new InventoryItem
        {
            Id = Guid.NewGuid(),
            Type = ItemType.Phone,
            Comment = "Test",
            PurchaseDate = DateTime.Now,
            UserId = Guid.NewGuid(),
            User = new User { FirstName = "Jonas", LastName = "Petraitis" }
        };

        var dto = InventoryMappers.MapItemToDto(item);

        dto.Type.Should().Be("Phone");
    }

    [Fact]
    public void MapItemToDto_CombinesUserFullName()
    {
        var item = new InventoryItem
        {
            Id = Guid.NewGuid(),
            Type = ItemType.Tablet,
            Comment = "Test",
            User = new User { FirstName = "Jonas", LastName = "Petraitis" }
        };

        var dto = InventoryMappers.MapItemToDto(item);

        dto.UserFullName.Should().Be("Jonas Petraitis");
    }

    [Fact]
    public void MapItemToDto_HandlesNullUser_ReturnsEmptyFullName()
    {
        var item = new InventoryItem
        {
            Id = Guid.NewGuid(),
            Type = ItemType.SimCard,
            Comment = "Test",
            User = null!
        };

        var dto = InventoryMappers.MapItemToDto(item);

        dto.UserFullName.Should().BeEmpty();
    }
}