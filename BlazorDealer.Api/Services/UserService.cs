using BlazorDealer.Models;
using BlazorDealer.Models.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDealer.Api.Services
{
    public interface IUserService
    {

        Task<UserManageResponse> RegisterUser(RegisterViewModel model);

        Task<UserManageResponse> LoginUser(LoginViewModel model);

    }

    public class UserService : IUserService
    {

        UserManager<IdentityUser> _userManager;
        IConfiguration _configuration;
        BlazorDealerDbContext _db;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration, BlazorDealerDbContext db)
        {
            _userManager = userManager;
            _configuration = configuration;
            _db = db;
        }

        /// <summary>
        /// Login the user via its Email and Password and generate the access token required to access protected resources
        /// </summary>
        /// <param name="model">Model that wraps the Username and Password of the user</param>
        /// <returns></returns>
        public async Task<UserManageResponse> LoginUser(LoginViewModel model)
        {
            // Get the user by name 
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                };

                var authSettings = _configuration.GetSection("AuthSettings");
                var signInKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings["Key"]));

                // Generate the token 
                var token = new JwtSecurityToken(
                    issuer: "http://ahmadmozaffar.net",
                    audience: "http://ahmadmozaffar.net",
                    expires: DateTime.Now.AddDays(30),
                    claims: claims,
                    signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature));

                return new UserManageResponse
                {
                    Message = new JwtSecurityTokenHandler().WriteToken(token),
                    IsSuccess = true,
                    ExpireDate = token.ValidTo
                };
            }

            return new UserManageResponse
            {
                IsSuccess = false,
                Message = "Username or password is invalid",
            };
        }

        /// <summary>
        /// Register a new user into AK Expenses and generate an account id 
        /// </summary>
        /// <param name="model">Model that wraps the requried user data of the user</param>
        /// <returns></returns>
        public async Task<UserManageResponse> RegisterUser(RegisterViewModel model)
        {
            // Validate the Models 
            string message = null;
            //if (!v.Validation.IsEmail(model.Email))
            //    message = "Invalid email address";
            if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.ConfirmPassword))
                message = "Invalid password or confirm password";
            if (model.Password != model.ConfirmPassword)
                message = "Entered password did not match the confirm password";

            if (message != null)
                return new UserManageResponse
                {
                    Message = message,
                    IsSuccess = false,
                };

            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Add user to the role 


                return new UserManageResponse
                {
                    Message = $"Welcome to Blazor Blog!",
                    IsSuccess = true,
                };
            }

            return new UserManageResponse
            {
                Message = "User did not create, There is something wrong",
                Errors = result.Errors.Select(e => e.Description),
                IsSuccess = false
            };
        }
    }
}
