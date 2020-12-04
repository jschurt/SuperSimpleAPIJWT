using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSimpleAPIJWT.FakeRepositories;
using SuperSimpleAPIJWT.Models;
using SuperSimpleAPIJWT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperSimpleAPIJWT.Controllers
{
    [Route("v1/account")]
    public class HomeController: ControllerBase
    {

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = FakeUserRepository.Get(model.UserName, model.Password);

            if (user == null)
                return NotFound(new { message = "Invalid User"});

            var token = TokenService.GenerateToken(user);
            user.Password = ""; //hidding password

            return new { user = user, token = token };

        }


        [HttpGet("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anonimous";

        [HttpGet("authenticated")]
        [Authorize]
        public string Authenticated() => $"Authenticated: {User.Identity.Name}";

        [HttpGet("employee")]
        [Authorize(Roles ="employee,manager")]
        public string Employee() => "Employee";

        [HttpGet("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Manager";


    } //class
} //namespace
