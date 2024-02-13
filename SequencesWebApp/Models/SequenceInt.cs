using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SequencesWebApp.Models
{
    public class SequenceInt
    {
        [Key]
        public int Id { get; set; }
        public int Value { get; set; }
        [ForeignKey("Sequence")]
        public int SequenceId { get; set; }

    }
}
