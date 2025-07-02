using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repository.Interface
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<Walk> GetByIdAsync(Guid id);
    }
}
