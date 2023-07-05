using API.Domain;

namespace API.Services.Interfaces
{
    public interface ITemporaryPasswordService
    {
        public void CreateAsync(TemporaryPassword temporaryPassword);
        public Task<Guid?> DeleteAsync(string password);
    }
}