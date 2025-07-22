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
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
    }
}
