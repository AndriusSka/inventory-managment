using FluentAssertions;
using InventoryManagement.Core.Dtos;
using InventoryManagement.Infrastructure.Repositories;
using InventoryManagement.Infrastructure.Services;
using InventoryManagement.Tests.TestHelpers;
using Xunit;

namespace InventoryManagement.Tests.Services;
// Service testai dirba per repository
public class InventoryItemServiceTests
{
    [Fact]
    public async Task GetFilteredAsync_ReturnsMappedDtos()
    {
        using var context = TestDbContextFactory.Create();
        var (jonas, _) = TestDbContextFactory.SeedUsers(context);
        TestDbContextFactory.SeedItems(context, jonas);
        var repository = new InventoryItemRepository(context);
        var service = new InventoryItemService(repository);

        var result = await service.GetFilteredAsync(new InventoryItemFilterDto());


        result.Should().HaveCount(2);
        result.First().UserFullName.Should().Be("Jonas Jonaitis");
    }

    [Fact]
    public async Task SoftDeleteAsync_ExistingItem_ReturnsTrue()
    {
        using var context = TestDbContextFactory.Create();
        var (jonas, _) = TestDbContextFactory.SeedUsers(context);
        var items = TestDbContextFactory.SeedItems(context, jonas);
        var repository = new InventoryItemRepository(context);
        var service = new InventoryItemService(repository);

        var result = await service.SoftDeleteAsync(items[0].Id);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task SoftDeleteAsync_NonExistingItem_ReturnsFalse()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new InventoryItemRepository(context);
        var service = new InventoryItemService(repository);

        var result = await service.SoftDeleteAsync(Guid.NewGuid());

        result.Should().BeFalse();
    }
}