using AutoMapper;
using COREAPI.DTOs;
using COREAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace COREAPI.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetUsers", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var users = new List<User>();
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PhoneNumber = reader.GetString(2),
                            Email = reader.GetString(3),
                            Gender = reader.GetString(4)
                        });
                    }
                    return _mapper.Map<IEnumerable<UserDTO>>(users);
                }
            }
        }

        public async Task<UserDTO> GetUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var user = new User
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            PhoneNumber = reader.GetString(2),
                            Email = reader.GetString(3),
                            Gender = reader.GetString(4)
                        };
                        return _mapper.Map<UserDTO>(user);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task CreateUser(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("CreateUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateUser(int id, UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
