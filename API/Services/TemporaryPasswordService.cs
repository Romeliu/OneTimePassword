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

        public void CreateAsync(TemporaryPassword temporaryPassword)
        {
            _repository.CreateAsync(temporaryPassword);
        }

        public async Task<Guid?> DeleteAsync(string token)
        {
            var passwordList = await _repository.GetAllAsync();
            TemporaryPassword matchedPassword = null;

            foreach (var password in passwordList)
            {
                if (ComparerHash(token, password))
                    matchedPassword = password;
            }

            if (matchedPassword != null)
            {
                _repository.DeleteAsync(matchedPassword);
                if (matchedPassword.CreatedAt < DateTime.Now.AddSeconds(-30))
                    return null;
                return matchedPassword.Id;
            }
            return null;
        }

        private bool ComparerHash(string token, TemporaryPassword password)
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