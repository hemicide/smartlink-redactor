using Microsoft.EntityFrameworkCore;
using Redactor.Domain.Entities;
using Redactor.Application.Interfaces;
using Redactor.Persistence.Contexts;

namespace Redactor.Persistence.Repositories
{
    public class SmartLinksRepository : ISmartLinksRepository
    {
        private readonly ModelContext _context;

        public SmartLinksRepository(ModelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Smartlinks>> GetAllAsync()
        {
            return await _context.Smartlinks.ToListAsync();
        }

        public async Task<Smartlinks> GetByIdAsync(Guid id)
        {
            return await _context.Smartlinks.FindAsync(id);
        }

        public async Task<Smartlinks?> GetByLinkAsync(string link)
        {
            return await _context.Smartlinks
                .Where(s => s.Link == link)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Smartlinks smartlink)
        {
            await _context.Smartlinks.AddAsync(smartlink);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Smartlinks smartlink) {
            _context.Smartlinks.Update(smartlink);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Smartlinks.FindAsync(id);
            if (user != null)
            {
                _context.Smartlinks.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
