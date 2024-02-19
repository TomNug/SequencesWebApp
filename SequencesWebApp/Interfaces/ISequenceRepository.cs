using SequencesWebApp.Models;

namespace SequencesWebApp.Interfaces
{
    public interface ISequenceRepository
    {
        Task<List<Sequence>> GetAllAsync();
        Task<Sequence?> GetByIdAsync(int id);
        Task<string> GetAllAsJsonAsync();
        bool Add(Sequence sequence);
        bool Delete(Sequence sequence);
        bool Save();
    }
}
