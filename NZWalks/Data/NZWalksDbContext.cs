using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data;

public class NzWalksDbContext : DbContext
{
    public NzWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
    {
        
    }

    public required DbSet<Difficulty> Difficulties { get; set; }
    public required DbSet<Region> Regions { get; set; }
    public required DbSet<Walk> Walks { get; set; }
}