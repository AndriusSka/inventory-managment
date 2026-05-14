using FluentAssertions;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
using InventoryManagement.Core.Mappers;
using Xunit;

namespace InventoryManagement.Tests.Mappers;

public class InventoryMappersTests
{
    [Fact]
    public void UserToDto_MapsAllFields()
    {
        // Paruošiami duomenys
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Aa",
            LastName = "Bb"
        };
        // Vykdomas veiksmas
        var dto = user.ToDto();
        // Patikrinami rezultatai
        dto.Id.Should().Be(user.Id);
        dto.FirstName.Should().Be("Aa");
        dto.LastName.Should().Be("Bb");
    }

    [Fact]
    public void InventoryItemToDto_ConvertsEnumToString()
    {
        var item = new InventoryItem
        {
            Id = Guid.NewGuid(),
            Type = ItemType.Phone,
            Comment = "Test",
            PurchaseDate = DateTime.Now,
            UserId = Guid.NewGuid(),
            User = new User { FirstName = "Aa", LastName = "Bb" }
        };

        var dto = item.ToDto();

        dto.Type.Should().Be("Phone");
    }

    [Fact]
    public void InventoryItemToDto_CombinesUserFullName()
    {
        var item = new InventoryItem
        {
            Id = Guid.NewGuid(),
            Type = ItemType.Tablet,
            Comment = "Test",
            User = new User { FirstName = "Aa", LastName = "Bb" }
        };

        var dto = item.ToDto();

        dto.UserFullName.Should().Be("Aa Bb");
    }

    [Fact]
    public void InventoryItemToDto_HandlesNullUser_ReturnsEmptyFullName()
    {
        var item = new InventoryItem
        {
            Id = Guid.NewGuid(),
            Type = ItemType.SimCard,
            Comment = "Test",
            User = null!
        };

        var dto = item.ToDto();

        dto.UserFullName.Should().BeEmpty();
    }
}