namespace API.Domain
{
    public class TemporaryPassword
    {
        public Guid Id { get; set; }
        public byte[] EncodedPasswordHash { get; set; }
        public byte[] EncodedPasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}