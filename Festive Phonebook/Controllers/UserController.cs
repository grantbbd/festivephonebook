using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Festive_Phonebook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Festive_Phonebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _options;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<ApplicationSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = options.Value;
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> Create(SignUpModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.UserName,
                UserName = model.UserName,
                FirstName = model.FirstName,
                Surname = model.Surname
            };

            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { token = GetToken(user) });
                }

                return BadRequest(result);

            } catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [Route("exists")]
        public async Task<ActionResult> HasUser(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                    return Ok();

                return NotFound();
            } catch (Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Ok(new { token = GetToken(user) });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }

        private string GetToken(ApplicationUser user)
        {
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                     new Claim[]
                     {
                            new Claim("UserID", user.Id.ToString())
                     }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(securityToken);
        }

    }
}