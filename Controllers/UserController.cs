using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            var user = _userRepo.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new {message = "Username or password is incorrect"});
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("Register")]

        public IActionResult Register([FromBody] AuthenticationModel model)
        {
            bool ifUserNameUnique = _userRepo.IsUniqueUser(model.Username);
            if (!ifUserNameUnique)
            {
                return BadRequest(new {message = "Username already exists"});
            }

            var user = _userRepo.Register(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new {message = "Error while registering"});
               
            }
            return Ok();
        }
    }

}
