namespace InventoryManagement.Core.Dtos;

public class InventoryItemDto
{
    public Guid Id { get; set; }
    // DTos'e saugoma string, nes frontendui paduodama strin reiksme
    public string Type { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public Guid UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
}