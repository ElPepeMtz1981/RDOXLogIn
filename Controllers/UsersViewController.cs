using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDOXMES.Login;

namespace Loging.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersViewController : ControllerBase
    {
        private readonly ViewUserDbContext _context;

        public UsersViewController(ViewUserDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("All")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        
        [Authorize(Roles = "SysAdmin")]
        [HttpGet("NameAndRol")]
        public async Task<IActionResult> GetUserAndRol()
        {
            var users = await _context.Users
                        .Select(u => new {
                            u.Name,
                            u.Rol
                        })
                        .ToListAsync();

            return Ok(users);
        }
    }
}
