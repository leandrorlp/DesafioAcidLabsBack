using AWSServerlessDesafioAcidLabs.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.DB.Operations
{
    public interface IUserCRUD
    {
        Task<string> AddUser(Users user);
        Task RemoveUser(string Id);
        Task<Users> GetUserById(string Id);
        Task<Users> GetUserByUsernameAndPassword(string username, string password);
        Task<List<Users>> GetAllUsers();
        Task UpdateUser(Users user);
        Task<Users> GetUserByRefresh(string refreshToken);
    }
}
