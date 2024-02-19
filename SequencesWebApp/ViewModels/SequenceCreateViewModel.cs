using SequencesWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SequencesWebApp.ViewModels
{
    public class SequenceCreateViewModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Sequences should contain at least one integer")]
        public List<int> Sequence { get; set; } = new List<int>();
        [Required]
        public bool IsAscending { get; set; }
        [Required]
        public float SortingTime { get; set; }
    }
}
