using Microsoft.EntityFrameworkCore;
using NZWalks.Models.Domain;

namespace NZWalks.Data;

public class NzWalksSqlServerDbContext : DbContext
{
    public NzWalksSqlServerDbContext(DbContextOptions<NzWalksSqlServerDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
        
    }

    public required DbSet<Difficulty> Difficulties { get; set; }
    public required DbSet<Region> Regions { get; set; }
    public required DbSet<Walk> Walks { get; set; }
}
