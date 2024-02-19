using SequencesWebApp.Models;

namespace SequencesWebApp.ViewModels
{
    public class SequenceDetailViewModel
    {
        public int Id { get; set; }
        public List<SequenceInt> Integers { get; set; } = new List<SequenceInt>();
        public bool IsAscending { get; set; }
        public float SortingTime { get; set; }
    }
}
