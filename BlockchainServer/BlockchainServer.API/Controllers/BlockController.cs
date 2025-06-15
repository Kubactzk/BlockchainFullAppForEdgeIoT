using BlockchainServer.Application.Services.Interfaces;
using BlockchainServer.Domain.Entities;
using BlockchainServer.Domain.Entities.Shared;
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
        public IActionResult PostData([FromBody] EdgeDeviceDataShared data)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            bool isAdded = _blockchainService.AddBlock(data);

            stopwatch.Stop();
            long elapsedMs = stopwatch.ElapsedMilliseconds;

            int count = data.Measurments?.Count ?? 0;

            string fileName = $"D:/Studia/ISA-magister/Magisterka/OfficialApplication/logs/{count}.txt";
            string logsDirectory = Path.GetDirectoryName(fileName);
            Directory.CreateDirectory(logsDirectory);

            System.IO.File.AppendAllText(fileName, $"{elapsedMs}\n");

            if (!isAdded)
            {
                return BadRequest("Block has not been added, wrong parameters.");
            }

            return Ok("Data received and block added successfully.");
        }


    }
}
