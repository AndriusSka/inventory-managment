using InventoryManagement.Core.Dtos;
using InventoryManagement.Core.Interfaces;
using InventoryManagement.Core.Mappers;

namespace InventoryManagement.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
    var users = await _repository.GetAllAsync();
    var dtos = new List<UserDto>();

    foreach (var user in users)
    {
        dtos.Add(InventoryMappers.MapUserToDto(user));
    }

    return dtos;
    }
}