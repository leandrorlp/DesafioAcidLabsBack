using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.DB.Entities
{
    public class Users
    {
        [DynamoDBHashKey]
        public Guid? Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string RefreshToken { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
