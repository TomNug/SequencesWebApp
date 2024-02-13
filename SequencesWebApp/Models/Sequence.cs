using System.ComponentModel.DataAnnotations;

namespace SequencesWebApp.Models
{
    public class Sequence
    {
        [Key]
        public int Id { get; set; }
        public List<int> Integers { get; set; }
        public bool IsAscending { get; set; }
        public float SortingTime { get; set; }
    }
}