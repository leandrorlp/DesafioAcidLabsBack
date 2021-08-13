using Amazon.DynamoDBv2.DataModel;
using AWSServerlessDesafioAcidLabs.DB.Entities;
using AWSServerlessDesafioAcidLabs.Models;
using AWSServerlessDesafioAcidLabs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.DB.Operations
{
    public class Auth : IAuth
    {
        private readonly IDynamoDBContext _context;
        private readonly IAuthService _auth;
        private readonly IUserCRUD _user;

        public Auth(IDynamoDBContext dynamoDbContext, IAuthService auth, IUserCRUD user)
        {
            _context = dynamoDbContext;
            this._auth = auth;
            this._user = user;
        }

        public async Task<LoginResponse> Login(string username, string password)
        {
            var user = await _user.GetUserByUsernameAndPassword(username, password);

            if(user == null)
            {
                return null;
            }

            return await GenerateToken(user);
        }

        public async Task<LoginResponse> RefreshToken(string refreshToken)
        {
            var user = await _user.GetUserByRefresh(refreshToken);

            if (user == null)
            {
                return null;
            }

            return await GenerateToken(user);
        }

        private async Task<LoginResponse> GenerateToken(Users user)
        {
            user.RefreshToken = Guid.NewGuid().ToString();
            await _user.UpdateUser(user);

            return new LoginResponse
            {
                Token = _auth.GenerateSecurityToken(user.Username),
                RefreshToken = user.RefreshToken
            };
        }
    }
}
