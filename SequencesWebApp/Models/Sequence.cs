using System.ComponentModel.DataAnnotations;

namespace SequencesWebApp.Models
{
    public class Sequence
    {
        [Key]
        public int Id { get; set; }
        public ICollection<SequenceInt> Integers { get; set; } = new List<SequenceInt>();
        public bool IsAscending { get; set; }
        public float SortingTime { get; set; }
    }
}