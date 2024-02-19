using SequencesWebApp.Models;

namespace SequencesWebApp.ViewModels
{
    public class SequenceDeleteViewModel
    {
        public int Id { get; set; }
        public List<SequenceInt> Integers { get; set; }
        public bool IsAscending { get; set; }
        public float SortingTime { get; set; }
    }
}
