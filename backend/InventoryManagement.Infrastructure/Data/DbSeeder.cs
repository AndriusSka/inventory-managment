using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;

namespace InventoryManagement.Infrastructure.Data;

// Dėl in-memory DB, sukuriu seed'erį, kuris užpildo duomenų bazę pradiniais duomenimis
public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        // Esant duomenims, nereikia seed'erio
        if (context.Users.Any())
            return;
        // Sukuriame keletą vartotojų
        var jonas = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Jonas",
            LastName = "Petraitis"
        };

        var ona = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Ona",
            LastName = "Kazlauskienė"
        };

        var marius = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "Marius",
            LastName = "Jankauskas"
        };

        context.Users.AddRange(jonas, ona, marius);

        // Sukuriame keletą inventoriaus item'ų, priskirdami juos vartotojams
        var items = new List<InventoryItem>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Type = ItemType.Laptop,
                Comment = "Darbo nešiojamas",
                PurchaseDate = new DateTime(2023, 5, 15),
                UserId = jonas.Id,
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = ItemType.Phone,
                Comment = "iPhone 14",
                PurchaseDate = new DateTime(2024, 1, 10),
                UserId = jonas.Id,
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = ItemType.Tablet,
                Comment = "Skirta klientų prezentacijoms",
                PurchaseDate = new DateTime(2024, 3, 20),
                UserId = ona.Id,
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = ItemType.SimCard,
                Comment = "Telia korporatyvinė",
                PurchaseDate = new DateTime(2023, 11, 5),
                UserId = ona.Id,
                IsActive = true
            },
            new()
            {
                Id = Guid.NewGuid(),
                Type = ItemType.Laptop,
                Comment = "Programuotojo darbinis",
                PurchaseDate = new DateTime(2024, 2, 28),
                UserId = marius.Id,
                IsActive = true
            }
        };

        context.InventoryItems.AddRange(items);
        // Išsaugome pakeitimus duomenų bazėje
        context.SaveChanges();
    }
}