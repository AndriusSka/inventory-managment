using FluentAssertions;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
using InventoryManagement.Infrastructure.Repositories;
using InventoryManagement.Tests.TestHelpers;
using Xunit;

namespace InventoryManagement.Tests.Repositories;
// Repository testai dirba tiesiogiai su DbContext, be jokio papildomo sluoksnio
public class InventoryItemRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_NoFilters_ReturnsAllActiveItems()
    {
        // using naudojamas, kad būtų užtikrinta, jog DbContext bus tinkamai išvalytas po kiekvieno testo
        using var context = TestDbContextFactory.Create();
        var (jonas, _) = TestDbContextFactory.SeedUsers(context);
        TestDbContextFactory.SeedItems(context, jonas);
        var repository = new InventoryItemRepository(context);
        
        var result = await repository.GetAllAsync(type: null, comment: null, userId: null);

        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetAllAsync_FilterByType_ReturnsMatchingItems()
    {
        using var context = TestDbContextFactory.Create();
        var (jonas, _) = TestDbContextFactory.SeedUsers(context);
        TestDbContextFactory.SeedItems(context, jonas);

        var repository = new InventoryItemRepository(context);

        var result = await repository.GetAllAsync(
            type: ItemType.Phone, comment: null, userId: null);

        result.Should().ContainSingle();
        result.First().Type.Should().Be(ItemType.Phone);
    }

    [Fact]
    public async Task GetAllAsync_FilterByCommentSubstring_ReturnsMatchingItems()
    {
        using var context = TestDbContextFactory.Create();
        var (jonas, _) = TestDbContextFactory.SeedUsers(context);
        TestDbContextFactory.SeedItems(context, jonas);

        var repository = new InventoryItemRepository(context);

        var result = await repository.GetAllAsync(
            type: null, comment: "iPhone", userId: null);

        result.Should().ContainSingle();
        result.First().Comment.Should().Contain("iPhone");
    }

    [Fact]
    public async Task GetAllAsync_FilterByUserId_ReturnsOnlyThatUsersItems()
    {
        using var context = TestDbContextFactory.Create();
        var (jonas, ona) = TestDbContextFactory.SeedUsers(context);
        TestDbContextFactory.SeedItems(context, jonas);

        context.InventoryItems.Add(new InventoryItem
        {
            Id = Guid.NewGuid(),
            Type = ItemType.Tablet,
            Comment = "Ona's tablet",
            PurchaseDate = DateTime.Now,
            UserId = ona.Id,
            IsActive = true
        });
        await context.SaveChangesAsync();
        var repository = new InventoryItemRepository(context);

        var result = await repository.GetAllAsync(
            type: null, comment: null, userId: jonas.Id);

        result.Should().HaveCount(2);
        result.Should().OnlyContain(i => i.UserId == jonas.Id);
    }

    [Fact]
    public async Task GetAllAsync_ExcludesSoftDeletedItems()
    {
        using var context = TestDbContextFactory.Create();
        var (jonas, _) = TestDbContextFactory.SeedUsers(context);
        var items = TestDbContextFactory.SeedItems(context, jonas);
        items[0].IsActive = false;
        await context.SaveChangesAsync();
        var repository = new InventoryItemRepository(context);

        var result = await repository.GetAllAsync(type: null, comment: null, userId: null);

        result.Should().HaveCount(1);
        result.First().Id.Should().Be(items[1].Id);
    }
}