using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;

namespace NZWalks.Repositories;

public class MySQLWalkRepository : IWalkRepository
{
    private readonly NZWalksDbContextMySQL dbContext;

    public MySQLWalkRepository(NZWalksDbContextMySQL dbContext)
    {
        this.dbContext = dbContext;
    }
    
    
    public async Task<Walk> CreateAsync(Walk walk)
    {
        await dbContext.Walks.AddAsync(walk);
        await dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAllWalksAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize)
    {
        var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
        
        //Filtering

        if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(w => w.Name.Contains(filterQuery));
            }
        }
        
        //Sorting

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(w => w.Name): walks.OrderByDescending(w => w.Name);
            }
            else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
            }
        }
        
        //Pagination
        var skipResults = (pageNumber - 1) * pageSize;
        
        
        
        return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        // return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walkRequest)
    {
        var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

        if (existingWalk == null)
        {
            return null;
        }

        ;
        existingWalk.Name = walkRequest.Name;
        existingWalk.Description = walkRequest.Description;
        existingWalk.LengthInKm = walkRequest.LengthInKm;
        existingWalk.RegionId = walkRequest.RegionId;
        existingWalk.WalkImageUrl = walkRequest.WalkImageUrl;
        existingWalk.DifficultyId = walkRequest.DifficultyId; 
        
        await dbContext.SaveChangesAsync();
        return existingWalk;
        
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var walkToDelete = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

        if (walkToDelete == null)
        {
            return null;    
        }
        dbContext.Walks.Remove(walkToDelete);
        await dbContext.SaveChangesAsync();
        return walkToDelete;
    }
}