using API.Data.Interfaces;
using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TemporaryPasswordRepository : ITemporaryPasswordRepository
    {
        private readonly DataContext _context;

        public TemporaryPasswordRepository(DataContext context)
        {
            _context = context;
            DeleteOldRecordsAsync();
        }

        private async void DeleteOldRecordsAsync()
        {
            var records = await _context.TemporaryPasswords.ToListAsync();
            foreach (var record in records)
            {
                if (record.CreatedAt.AddSeconds(30).CompareTo(DateTime.Now) < 0)
                    _context.TemporaryPasswords.Remove(record);
            }
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(TemporaryPassword password)
        {
            _context.TemporaryPasswords.Add(password);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid?> DeleteAsync(TemporaryPassword temporaryPassword)
        {
            Guid Id = temporaryPassword.Id;
            _context.TemporaryPasswords.Remove(temporaryPassword);
            await _context.SaveChangesAsync();
            return Id;
        }

        public async Task<List<TemporaryPassword>> GetAllAsync()
        {
            return await _context.TemporaryPasswords.ToListAsync();
        }
    }
}