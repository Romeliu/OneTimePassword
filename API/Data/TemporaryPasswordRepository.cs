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
                if (record.CreatedAt < DateTime.Now.AddSeconds(-30))
                    _context.TemporaryPasswords.Remove(record);
            }
            await _context.SaveChangesAsync();
        }

        public async void CreateAsync(TemporaryPassword password)
        {
            _context.Add(password);
            await _context.SaveChangesAsync();
        }

        public async void DeleteAsync(TemporaryPassword temporaryPassword)
        {
            _context.TemporaryPasswords.Remove(temporaryPassword);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TemporaryPassword>> GetAllAsync()
        {
            return await _context.TemporaryPasswords.ToListAsync();
        }
    }
}