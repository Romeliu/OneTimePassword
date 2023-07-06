using System.Security.Cryptography;
using System.Text;
using API.Data.Interfaces;
using API.Domain;
using API.Services.Interfaces;

namespace API.Services
{
    public class TemporaryPasswordService : ITemporaryPasswordService
    {
        private readonly ITemporaryPasswordRepository _repository;
        public TemporaryPasswordService(ITemporaryPasswordRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(string userId, string token, DateTime createdAt)
        {
            using var hmac = new HMACSHA512();

            var temporaryPassword = new TemporaryPassword { CreatedAt = createdAt };

            temporaryPassword.EncodedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));
            temporaryPassword.EncodedPasswordSalt = hmac.Key;

            await _repository.CreateAsync(temporaryPassword);
        }

        public async Task<Guid?> DeleteAsync(string token)
        {
            var passwordList = await _repository.GetAllAsync();
            TemporaryPassword matchedPassword = null;

            foreach (var password in passwordList)
            {
                if (CompareHash(token, password))
                {
                    matchedPassword = password;
                    break;
                }
            }

            if (matchedPassword != null)
            {
                if (matchedPassword.CreatedAt.AddSeconds(30).CompareTo(DateTime.Now) < 0)
                {
                    await _repository.DeleteAsync(matchedPassword);
                    return null;
                }
                return await _repository.DeleteAsync(matchedPassword);
            }
            return null;
        }

        private bool CompareHash(string token, TemporaryPassword password)
        {
            using var hmac = new HMACSHA512(password.EncodedPasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (password.EncodedPasswordHash[i] != computedHash[i])
                    return false;
            }
            return true;
        }
    }
}