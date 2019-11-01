using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDealer.Api.Services;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDealer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService; 
        }


        // POST: auth/register
        [Route("Register")]
        [HttpPost()]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUser(model);

                if (result.IsSuccess)
                {

                    return Ok(result);
                }

                return BadRequest("Invalid Username or password");
            }

            return BadRequest("Invalid data properties"); 
        }

        // POST: auth/login
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest(new UserManageResponse
            {
                Message = "Username or password is invalid",
                IsSuccess = false
            });
        }

    }
}