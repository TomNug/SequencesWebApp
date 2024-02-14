using Microsoft.AspNetCore.Mvc;
using SequencesWebApp.Interfaces;
using SequencesWebApp.Models;
using SequencesWebApp.ViewModels;

namespace SequencesWebApp.Controllers
{
    public class SequenceController : Controller
    {
        private readonly ISequenceRepository _sequenceRepository;
        public SequenceController(ISequenceRepository sequenceRepository)
        {
            _sequenceRepository = sequenceRepository;

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

        public async Task<IActionResult> Detail(int id)
        {
            Sequence sequence = await _sequenceRepository.GetByIdAsync(id);
            SequenceDetailViewModel sequenceDetailViewModel = new SequenceDetailViewModel()
            {
                Id = id,
                Integers = sequence.Integers.ToList(),
                IsAscending = sequence.IsAscending,
                SortingTime = sequence.SortingTime
            };
            return View(sequenceDetailViewModel);
        }

        //[HttpGet]
        public IActionResult Create()
        {
            List<int> integerSequence = new List<int>();
            return View(integerSequence);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SequenceCreateViewModel sequenceCreateViewModel)
        {
            List<int> ints = sequenceCreateViewModel.Sequence;
            //bool ascending = sequenceCreateViewModel.IsAscending;
            //float timeTaken = sequenceCreateViewModel.SortingTime;
            return Ok();
        }
    }
}
