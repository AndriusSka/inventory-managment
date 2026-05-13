using InventoryManagement.Core.Enums;

namespace InventoryManagement.Core.Entities;

public class InventoryItem
{
    public Guid Id { get; set; }
    public ItemType Type { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public bool IsActive { get; set; } = true;  // soft delete

    // Nustatoma kuriam vartotojui priklauso item'as
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;  // del nullable warning, kompiliatoriui pranesama, kad reiksme nebus null
}