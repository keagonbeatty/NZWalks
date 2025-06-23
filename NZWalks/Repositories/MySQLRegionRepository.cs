using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public class MySqlRegionRepository: IRegionRepository
{
    private readonly NZWalksDbContextMySQL _dbContext;
    public MySqlRegionRepository(NZWalksDbContextMySQL dbContext)
    {
        this._dbContext = dbContext;
    }
    public async Task<List<Region>> GetAllAsync()
    {
        return await _dbContext.Regions.ToListAsync();
        
    }

    public Task<Region?> GetByIdAsync(Guid id)
    {
        return _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Region> CreateAsync(Region region)
    {
       await _dbContext.Regions.AddAsync(region);
       await _dbContext.SaveChangesAsync();
       return region;
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (existingRegion == null)
        {
            return null;
        }
        
        existingRegion.Name = region.Name;
        existingRegion.Code = region.Code;
        existingRegion.RegionImageUrl = region.RegionImageUrl;
        
        await _dbContext.SaveChangesAsync();
        
        return existingRegion;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var existingRegion = await _dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        if (existingRegion == null)
        {
            return null;
        }
        
        _dbContext.Regions.Remove(existingRegion);
        await _dbContext.SaveChangesAsync();
        
        return existingRegion;
    }
}