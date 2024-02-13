using ListsWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SequencesWebApp.Interfaces;
using SequencesWebApp.Models;
using SequencesWebApp.ViewModels;
using System.Diagnostics;

namespace SequencesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISequenceRepository _sequenceRepository;

        public HomeController(ILogger<HomeController> logger, ISequenceRepository sequenceRespository)
        {
            _logger = logger;
            _sequenceRepository = sequenceRespository;
        }

        public async Task<IActionResult> Index()
        {
            // Check if null
            var sequences = await _sequenceRepository.GetAllAsync(); 
                

            // Could sequences be null
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Sequences = sequences
            };

            return View(homeViewModel);
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
    }
}
