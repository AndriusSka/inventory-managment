using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Tests.TestHelpers;

public static class TestDbContextFactory
{
    public static AppDbContext Create()
    {
        // Kiekvienam testui naudojamas unikalus InMemory duomenų bazės pavadinimas, kad būtų užtikrinama izoliuotą testavimo aplinką
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    // Ši funkcija sukuria du vartotojus ir išsaugo juos duomenų bazėje, grąžindama sukurtus vartotojus 
    public static (User andrius, User jonas) SeedUsers(AppDbContext context)
    {
        var jonas = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Jonas",
            LastName = "Jonaitis"
        };
        var ona = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Ona",
            LastName = "Onaitė"
        };

        context.Users.AddRange(jonas, ona);
        context.SaveChanges();

        return (jonas, ona);
    }
    // Ši funkcija sukuria du inventoriaus įrašus, susietus su nurodytu vartotoju, ir išsaugo juos duomenų bazėje
    public static List<InventoryItem> SeedItems(AppDbContext context, User user)
    {
        var items = new List<InventoryItem>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Type = ItemType.Phone,
                Comment = "iPhone 14",
                PurchaseDate = new DateTime(2024, 1, 10),
                UserId = user.Id,
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = ItemType.Laptop,
                Comment = "MacBook Pro",
                PurchaseDate = new DateTime(2024, 2, 15),
                UserId = user.Id,
                IsActive = true
            }
        };

        context.InventoryItems.AddRange(items);
        context.SaveChanges();

        return items;
    }
}