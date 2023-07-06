using API.Domain;

namespace API.Services.Interfaces
{
    public interface ITemporaryPasswordService
    {
        public Task CreateAsync(string userId, string token, DateTime createdAt);
        public Task<Guid?> DeleteAsync(string password);
    }
}