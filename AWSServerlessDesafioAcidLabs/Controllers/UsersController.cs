using AWSServerlessDesafioAcidLabs.DB.Entities;
using AWSServerlessDesafioAcidLabs.DB.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerlessDesafioAcidLabs.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserCRUD _users;

        public UsersController(IUserCRUD users)
        {
            _users = users;
        }

        [ProducesResponseType(typeof(List<Users>), 200)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _users.GetAllUsers());
        }

        [ProducesResponseType(typeof(Users), 200)]
        [HttpGet("{id}")]
        public async Task<Users> Get(string id)
        {
            return await _users.GetUserById(id);
        }

        [ProducesResponseType(typeof(string), 200)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users value)
        {
            var result = await _users.AddUser(value);

            if(result == null)
            {
                return BadRequest();
            }

            return Ok(new { result = result });
        }


        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(Users value)
        {
            await _users.UpdateUser(value);

            return Ok(new { result = true });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _users.RemoveUser(id);

            return Ok();
        }
    }
}
