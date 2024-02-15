using SequencesWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SequencesWebApp.ViewModels
{
    public class SequenceCreateViewModel
    {
        //public int Id { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Sequences should contain at least one integer")]
        public List<int> Sequence { get; set; }
        [Required]
        public bool IsAscending { get; set; }
        [Required]
        public float SortingTime { get; set; }
    }
}
