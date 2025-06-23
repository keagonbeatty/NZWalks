using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Data;

public class NZWalksAuthDbContext : IdentityDbContext
{
    public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options): base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var reader = "6ef5a623-8f2f-4f9d-aa40-0476dcf058aa";
        var writer = "966f5fdb-3c1c-4f96-98cb-076c05782b21";

        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = reader,
                ConcurrencyStamp = reader,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper()
            },
            new IdentityRole
            {
                Id = writer,
                ConcurrencyStamp = writer,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper()
            }
        };
        
        builder.Entity<IdentityRole>().HasData(roles);
    }
}