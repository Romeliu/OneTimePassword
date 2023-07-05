using API.Domain;

namespace API.Data.Interfaces
{
    public interface ITemporaryPasswordRepository
    {
        public void CreateAsync(TemporaryPassword password);

        public Task<List<TemporaryPassword>> GetAllAsync();

        public void DeleteAsync(TemporaryPassword temporaryPassword);
    }
}