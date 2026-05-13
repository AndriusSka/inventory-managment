using InventoryManagement.Core.Dtos;

namespace InventoryManagement.Core.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
}