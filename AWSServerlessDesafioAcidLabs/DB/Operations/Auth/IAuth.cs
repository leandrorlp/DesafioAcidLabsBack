using AWSServerlessDesafioAcidLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.DB.Operations
{
    public interface IAuth
    {
        Task<LoginResponse> Login(string username, string password);
        Task<LoginResponse> RefreshToken(string refreshToken);
    }
}
