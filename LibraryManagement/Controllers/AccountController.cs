using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryManagmentDomainLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryManagement.Controllers
{
    [Route("api/account")]
    [Consumes("application/json")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // GET: /<controller>/
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          RoleManager<IdentityRole> roleManager,
          IConfiguration configuration
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                return await GenerateJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }


        

        [HttpPost("register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {

            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
                
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(user.UserName);

               await _signInManager.SignInAsync(user, false);

              
               var roleresult = await _userManager.AddToRoleAsync(currentUser, "AppUser");
              return await GenerateJwtToken(model.Email, user);
            }

           throw new ApplicationException("UNKNOWN_ERROR");
        }


        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            public UserType UserType { get; set; }
            
           
        }

        public class LoginDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

        }

        private async Task<object> GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = GetUserClaims(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
              _configuration["JwtIssuer"],
              _configuration["JwtIssuer"],
              claims,
              expires: expires,
              signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private IEnumerable<Claim> GetUserClaims(ApplicationUser user)
        {
            IEnumerable<Claim> claims = new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim("USERID", user.Id),
                    new Claim("EMAIL", user.Email)
                    };
            return claims;
        }

        private async Task createRolesandUsers()
        {
            bool x = await _roleManager.RoleExistsAsync("Admin");
            if (!x)
            {
                            

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";

                string userPWD = "Password@12345";

                
                 await  _roleManager.CreateAsync(new IdentityRole("Admin"));
               //}
            }

            // creating Creating Manager role     
            x = await _roleManager.RoleExistsAsync("AppUser");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "AppUser";
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
