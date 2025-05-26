using BlockchainServer.Application.Services.Interfaces;
using BlockchainWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BlockchainServer.Domain.Entities;

namespace BlockchainWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlockchainService _blockchainService;
        public HomeController(ILogger<HomeController> logger, IBlockchainService blockchainService)
        {
            _blockchainService = blockchainService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var chain = _blockchainService.GetBlockchain();
            return View(chain);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CheckBlockchainValidity()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult VerifySignatures()
        {
            return RedirectToAction("Index");
        }
    }
}
