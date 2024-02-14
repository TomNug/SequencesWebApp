using ListsWebApp.Data;
using Microsoft.EntityFrameworkCore;
using SequencesWebApp.Interfaces;
using SequencesWebApp.Models;

namespace SequencesWebApp.Repositories
{
    public class SequenceIntRepository : ISequenceIntRepository
    {
        private readonly ApplicationDbContext _context;
        public SequenceIntRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(SequenceInt sequenceInt)
        {
            _context.Add(sequenceInt);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
