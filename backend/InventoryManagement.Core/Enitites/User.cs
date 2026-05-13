namespace InventoryManagement.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    //Vienas vartotojas gali turėti daug item'u
    public ICollection<InventoryItem> Items { get; set; } = new List<InventoryItem>();
}