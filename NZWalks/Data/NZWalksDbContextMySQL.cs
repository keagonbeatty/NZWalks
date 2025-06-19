using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NZWalks.Models.Domain;

namespace NZWalks.Data
{
    public class NZWalksDbContextMySQL : DbContext
    {
        public NZWalksDbContextMySQL(DbContextOptions<NZWalksDbContextMySQL> dbContextOptions)
        : base(dbContextOptions) { }

        public required DbSet<Difficulty> Difficulties { get; set; }
        public required DbSet<Region> Regions { get; set; }
        public required DbSet<Walk> Walks { get; set; }
    }
}

