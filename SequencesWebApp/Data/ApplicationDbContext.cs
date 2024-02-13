using Microsoft.EntityFrameworkCore;
using SequencesWebApp.Models;

namespace ListsWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Sequence> Sequences { get; set; }
    }
}
