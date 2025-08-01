using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDOXMES.Login;

namespace RDOXMES.ViewUsers
{
    [ApiController]
    [Route("api/viewusers")]
    public class ViewUsersController : ControllerBase
    {
        private readonly ViewUserDbContext _context;

        public ViewUsersController(ViewUserDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "SysAdmin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
        
        [Authorize(Roles = "SysAdmin")]
        [HttpGet("nameandrol")]
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
