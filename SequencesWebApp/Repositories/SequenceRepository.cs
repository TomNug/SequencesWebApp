using ListsWebApp.Data;
using Microsoft.EntityFrameworkCore;
using SequencesWebApp.Interfaces;
using SequencesWebApp.Models;

namespace SequencesWebApp.Repositories
{
    public class SequenceRepository : ISequenceRepository
    {
        private readonly ApplicationDbContext _context;
        public SequenceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Sequence sequence)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Sequence sequence)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sequence>> GetAllAsync()
        {
            return await _context.Sequences.Include(s => s.Integers).ToListAsync();
        }

        public Task<Sequence> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
