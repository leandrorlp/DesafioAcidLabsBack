using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AWSServerlessDesafioAcidLabs.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.DB.Operations
{
    public class UserCRUD : IUserCRUD
    {
        private readonly IDynamoDBContext _context;
        public UserCRUD(IDynamoDBContext dynamoDbContext)
        {
            _context = dynamoDbContext;
        }
        public async Task<string> AddUser(Users user)
        {
            var conditions = new List<ScanCondition>();

            conditions.Add(new ScanCondition("Username", ScanOperator.Equal, user.Username));

            var result = await _context.ScanAsync<Users>(conditions).GetRemainingAsync();

            if (result.Any())
            {
                return null;
            }

            user.Id = Guid.NewGuid();
            await _context.SaveAsync(user);

            return user.Id.ToString();
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _context.ScanAsync<Users>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<Users> GetUserById(string Id)
        {
            return await _context.LoadAsync<Users>(Id);
        }

        public async Task<Users> GetUserByUsernameAndPassword(string username, string password)
        {
            var conditions = new List<ScanCondition>();

            conditions.Add( new ScanCondition("Username", ScanOperator.Equal, username));
            conditions.Add(new ScanCondition("Password", ScanOperator.Equal, password));
            conditions.Add(new ScanCondition("Active", ScanOperator.Equal, true));

            var result = await _context.ScanAsync<Users>(conditions).GetRemainingAsync();

            if(result.Any())
            {
                return result.First();
            }

            return null;
        }

        public async Task<Users> GetUserByRefresh(string refreshToken)
        {
            var conditions = new List<ScanCondition>();

            conditions.Add(new ScanCondition("refresh-token", ScanOperator.Equal, refreshToken));
            conditions.Add(new ScanCondition("Active", ScanOperator.Equal, true));

            var result = await _context.ScanAsync<Users>(conditions).GetRemainingAsync();

            if (result.Any())
            {
                return result.First();
            }

            return null;
        }

        public async Task RemoveUser(string Id)
        {
            var conditions = new List<ScanCondition>();

            conditions.Add(new ScanCondition("Username", ScanOperator.Equal, "admin"));

            var result = await _context.ScanAsync<Users>(conditions).GetRemainingAsync();

            if(result.Any() && result.First().Id.Value.ToString() == Id)
            {
                return;
            }

            await _context
                .DeleteAsync<Users>(Id);
        }

        public async Task UpdateUser(Users user)
        {
            var userDB = await _context
                .LoadAsync<Users>(user.Id);

            userDB.Active = user.Active;
            userDB.Name = user.Name;
            userDB.Password = user.Password;
            userDB.RefreshToken = null;
            userDB.Username = user.Username;

            await _context.SaveAsync(userDB);
        }
    }
}
