using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
