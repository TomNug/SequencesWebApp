using Microsoft.AspNetCore.Mvc;
using SequencesWebApp.Interfaces;
using SequencesWebApp.Models;
using SequencesWebApp.ViewModels;
using System.Net;

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
            // Either populated, or empty list
            var sequences = await _sequenceRepository.GetAllAsync();

            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Sequences = sequences
            };

            return View(homeViewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Sequence? sequence = await _sequenceRepository.GetByIdAsync(id);

            if (sequence == null)
            {
                return NotFound();
            }
            else
            {
                var sequenceDetailViewModel = new SequenceDetailViewModel()
                {
                    Id = id,
                    Integers = sequence.Integers.ToList(),
                    IsAscending = sequence.IsAscending,
                    SortingTime = sequence.SortingTime
                };
                return View(sequenceDetailViewModel);
            }
            
        }

        public IActionResult Create()
        {
            return View();
        }

        // JavaScript will call this action from the Create View
        [HttpPost]
        public IActionResult Create([FromBody] SequenceCreateViewModel sequenceCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                List<int> ints = sequenceCreateViewModel.Sequence;
                bool ascending = sequenceCreateViewModel.IsAscending;
                float timeTaken = sequenceCreateViewModel.SortingTime;

                List<SequenceInt> integerList = new List<SequenceInt>();

                // Create each SequenceInt
                // (the integers which form the sequence)
                foreach (int integer in ints)
                {
                    SequenceInt sequenceInt = new SequenceInt()
                    {
                        Value = integer,
                    };
                    integerList.Add(sequenceInt);
                }

                // Create new Sequence
                var sequence = new Sequence()
                {
                    IsAscending = ascending,
                    SortingTime = timeTaken,
                    Integers = integerList
                };

                _sequenceRepository.Add(sequence);
                TempData["SaveSuccessMessage"] = "Sequence saved successfully.";
                return Ok(new { message = "Sequence saved." });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        public async Task<IActionResult> JSON(JSONViewModel jSONViewModel)
        {
            // Retrieve JSON string from repository
            string jsonData = await _sequenceRepository.GetAllAsJsonAsync();
            // Show on page
            return Content(jsonData, "application/json");
        }



        public async Task<IActionResult> DownloadJsonFile()
        {
            // Retrieve JSON string from repository
            var jsonData = await _sequenceRepository.GetAllAsJsonAsync();


            string fileName = "sequences.json";
            string contentType = "application/json";

            // Convert to byte array
            byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonData);

            // Return file for download
            return File(data, contentType, fileName);
        }


        


        public async Task<IActionResult> Delete(int Id)
        {
            var sequenceDetails = await _sequenceRepository.GetByIdAsync(Id);
            if (sequenceDetails == null)
            {
                // Entity does not exist
                return NotFound(); 
            }
            return View(sequenceDetails);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var sequenceDetails = await _sequenceRepository.GetByIdAsync(Id);
            if (sequenceDetails == null)
            {
                // Sequence does not exist
                return NotFound();
            }

            _sequenceRepository.Delete(sequenceDetails);
            TempData["DeleteSuccessMessage"] = "Sequence deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
