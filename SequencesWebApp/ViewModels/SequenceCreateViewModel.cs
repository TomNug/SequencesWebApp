using SequencesWebApp.Models;
using SequencesWebApp.Models.Validators;
using System.ComponentModel.DataAnnotations;

namespace SequencesWebApp.ViewModels
{
    public class SequenceCreateViewModel
    {
        [Required]
        [AtLeastOneIntegerElement]
        public List<int> Sequence { get; set; } = new List<int>();
        [Required]
        public bool IsAscending { get; set; }
        [Required]
        public float SortingTime { get; set; }
    }
}
