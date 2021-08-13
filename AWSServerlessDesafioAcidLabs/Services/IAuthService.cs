using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.Services
{
    public interface IAuthService
    {
        string GenerateSecurityToken(string email);
    }
}
