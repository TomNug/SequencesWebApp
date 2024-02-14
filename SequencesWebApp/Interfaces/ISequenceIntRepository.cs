using SequencesWebApp.Models;

namespace SequencesWebApp.Interfaces
{
    public interface ISequenceIntRepository
    {
        bool Add(SequenceInt sequenceInt);
        bool Save();
    }
}
