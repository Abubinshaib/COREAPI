using COREAPI.DTOs;

namespace COREAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> GetUser(int id);
        Task CreateUser(UserDTO userDto);
        Task UpdateUser(int id, UserDTO userDto);
        Task DeleteUser(int id);
    }
}
