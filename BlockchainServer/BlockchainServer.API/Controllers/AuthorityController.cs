using BlockchainServer.Domain.Entities.DatabaseModels;
using BlockchainServer.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlockchainServer.API.Controllers
{
    public class AuthorityController : Controller
    {
        private readonly AppDbContext _context;
        public AuthorityController(AppDbContext context) 
        {
            _context = context;
        }

        [Route("api/authority")]
        [HttpPost]
        public IActionResult PostName([FromBody] Device device)
        {
            device.IsActive = true;
            device.IsAuthority = true;
            _context.Devices.Add(device);
            _context.SaveChanges();

            return Ok("Successfully added authority");
        }
    }
}
