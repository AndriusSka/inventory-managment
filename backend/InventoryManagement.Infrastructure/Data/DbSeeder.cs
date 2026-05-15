using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;

namespace InventoryManagement.Infrastructure.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        // Jei duomenų bazėje jau yra vartotojų, praleidžiame seed'inimą
        if (context.Users.Any())
        {
            return;
        }

        var users = CreateUsers();
        context.Users.AddRange(users);

        var items = CreateItems(users);
        context.InventoryItems.AddRange(items);

        context.SaveChanges();
    }

    private static List<User> CreateUsers()
    {
        return new List<User>
        {
            new() { Id = Guid.NewGuid(), FirstName = "Jonas", LastName = "Jonaitis" },
            new() { Id = Guid.NewGuid(), FirstName = "Ona", LastName = "Onaite" },
            new() { Id = Guid.NewGuid(), FirstName = "Petras", LastName = "Petraitis" },
            new() { Id = Guid.NewGuid(), FirstName = "Marija", LastName = "Marijoniene" },
            new() { Id = Guid.NewGuid(), FirstName = "Tomas", LastName = "Tomaitis" },
            new() { Id = Guid.NewGuid(), FirstName = "Egle", LastName = "Egliene" }
        };
    }

    private static List<InventoryItem> CreateItems(List<User> users)
    {
        var items = new List<InventoryItem>
        {
            CreateItem(ItemType.Laptop, "MacBook Pro M3", new DateTime(2024, 1, 15), users[0]),
            CreateItem(ItemType.Phone, "iPhone 15 Pro", new DateTime(2024, 2, 10), users[0]),
            CreateItem(ItemType.SimCard, "Telia korporatyvine", new DateTime(2024, 2, 10), users[0]),
            CreateItem(ItemType.Laptop, "MacBook Air M2", new DateTime(2023, 11, 20), users[1]),
            CreateItem(ItemType.Tablet, "iPad Pro klientu prezentacijoms", new DateTime(2024, 3, 5), users[1]),
            CreateItem(ItemType.Laptop, "Dell XPS 15", new DateTime(2023, 8, 12), users[2]),
            CreateItem(ItemType.Phone, "Samsung Galaxy S24", new DateTime(2024, 1, 30), users[2]),
            CreateItem(ItemType.SimCard, "Bite korporatyvine", new DateTime(2024, 1, 30), users[2]),

            CreateItem(ItemType.Phone, "iPhone 14", new DateTime(2023, 6, 18), users[3]),
            CreateItem(ItemType.Tablet, "iPad Air socialiniu tinklu valdymui", new DateTime(2024, 4, 2), users[3]),

            CreateItem(ItemType.Laptop, "Lenovo ThinkPad X1", new DateTime(2024, 2, 28), users[4]),
            CreateItem(ItemType.Phone, "Google Pixel 8", new DateTime(2024, 3, 15), users[4]),

            CreateItem(ItemType.Phone, "iPhone SE", new DateTime(2023, 9, 1), users[5]),
            CreateItem(ItemType.SimCard, "Tele2 korporatyvine", new DateTime(2023, 9, 1), users[5])
        };

        return items;
    }

    private static InventoryItem CreateItem(ItemType type, string comment, DateTime purchaseDate, User user)
    {
        return new InventoryItem
        {
            Id = Guid.NewGuid(),
            Type = type,
            Comment = comment,
            PurchaseDate = purchaseDate,
            UserId = user.Id,
            IsActive = true
        };
    }
}