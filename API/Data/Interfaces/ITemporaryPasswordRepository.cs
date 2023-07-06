using API.Domain;

namespace API.Data.Interfaces
{
    public interface ITemporaryPasswordRepository
    {
        public Task CreateAsync(TemporaryPassword password);

        public Task<List<TemporaryPassword>> GetAllAsync();

        public Task<Guid?> DeleteAsync(TemporaryPassword temporaryPassword);
    }
}