using SequencesWebApp.Models;

namespace SequencesWebApp.ViewModels
{
    public class SequenceCreateViewModel
    {
        //public int Id { get; set; }
        public List<int> Sequence { get; set; }
        public bool IsAscending { get; set; }
        public float SortingTime { get; set; }
    }
}
