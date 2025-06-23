using NZWalks.Models.Domain;
using NZWalks.Models.DTOs;

namespace NZWalks.Repositories;

public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);
    Task<List<Walk>> GetAllWalksAsync(string? filterOn, string? filterQuery
    , string? sortBy, bool isAscending, int pageNumber, int pageSize);
    
    Task<Walk?> GetByIdAsync(Guid id);
    
    Task<Walk?> UpdateAsync(Guid id, Walk walkRequest);
    
    Task<Walk?> DeleteAsync(Guid id);

}