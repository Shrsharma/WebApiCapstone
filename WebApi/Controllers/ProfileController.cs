using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private UserManager<AuthModel> _userManager;
        private readonly AppDbContext _context;

        public ProfileController(UserManager<AuthModel> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        //GET: api/Profile

        public async Task<Object> GetUserProfile()
        {
            string userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return Ok(new
            {
                user.UserName,
                user.Email,
                user.FullName,
                user.PhoneNumber
            });
        }

        [HttpGet("Details")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AuthModel>>> GetUserDetails()
        {
            return await _context.AuthModel.ToListAsync();
        }


    }
}
