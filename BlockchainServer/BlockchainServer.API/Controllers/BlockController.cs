using BlockchainServer.Application.Services.Interfaces;
using BlockchainServer.Domain.Entities;
using BlockchainServer.Domain.Entities.DatabaseModels;
using BlockchainServer.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainServer.API.Controllers
{
    [ApiController]
    [Route("api/block")]
    public class BlockController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IBlockchainService _blockchainService;
        public BlockController(AppDbContext context, IBlockchainService blockchainService)
        {
            _context = context;
            _blockchainService = blockchainService;
        }

        [HttpPost]
        public IActionResult PostData([FromBody] EdgeDeviceData data)
        {
            _blockchainService.AddBlock(data);
            List<Block> blocks = _blockchainService.GetBlockchain();
            Console.WriteLine(blocks.Count);
            return Ok("Data received and block added successfully.");
        }
    }
}
