namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(string userId, DateTime createdAt);
    }
}