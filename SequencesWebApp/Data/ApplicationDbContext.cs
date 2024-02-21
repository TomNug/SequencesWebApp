using Microsoft.EntityFrameworkCore;
using SequencesWebApp.Models;

namespace SequencesWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Sequence> Sequences { get; set; }
    }
}
