using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
using InventoryManagement.Core.Interfaces;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories;

public class InventoryItemRepository : IInventoryItemRepository
{
    private readonly AppDbContext _context;

    public InventoryItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InventoryItem>> GetAllAsync(
    ItemType? type, string? comment, Guid? userId)
    {
        // Pradine uzklausa 
        IQueryable<InventoryItem> query = _context.InventoryItems
            .Include(i => i.User);

        // Pridedam filtrus
        if (type.HasValue)
        {
            query = query.Where(i => i.Type == type.Value);
        }

        if (!string.IsNullOrWhiteSpace(comment))
        {
            query = query.Where(i => i.Comment.Contains(comment));
        }

        if (userId.HasValue)
        {
            query = query.Where(i => i.UserId == userId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<InventoryItem?> GetByIdAsync(Guid id)
    {
        return await _context.InventoryItems
            .Include(i => i.User)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        if (item != null)
        {
            item.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
}