﻿using ListsWebApp.Data;
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
            _context.Add(sequence);
            return Save();
        }

        public bool Delete(Sequence sequence)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sequence>> GetAllAsync()
        {
            return await _context.Sequences.Include(s => s.Integers).ToListAsync();
        }

        public async Task<Sequence> GetByIdAsync(int id)
        {
            return await _context.Sequences.Include(s => s.Integers).SingleOrDefaultAsync(s => s.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
