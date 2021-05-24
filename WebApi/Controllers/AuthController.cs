using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<AuthModel> _userManager;
        private SignInManager<AuthModel> _signInManger;

        public AuthController(UserManager<AuthModel> userManager, SignInManager<AuthModel> signInManager)
        {
            _userManager = userManager;
            _signInManger = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        //POST: "/api/[controller]/Register"
        public async Task<Object> PostAuth(AuthUserModel model)
        {
            var authUser = new AuthModel()
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(authUser, model.Password);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Signin")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            var key = Encoding.ASCII.GetBytes("This is a Secret key which I am using for my API");

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDesc = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id)
                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Expires = DateTime.Now.AddHours(2)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDesc);
                var token = tokenHandler.WriteToken(createdToken);
                return Ok(new { token });
            }

            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
