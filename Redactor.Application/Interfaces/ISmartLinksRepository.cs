
using Redactor.Domain.Entities;

namespace Redactor.Application.Interfaces
{
    public interface ISmartLinksRepository
    {
        public Task<IEnumerable<Smartlinks>> GetAllAsync();

        public Task<Smartlinks> GetByIdAsync(Guid id);

        public Task<Smartlinks> GetByLinkAsync(string link);

        public Task AddAsync(Smartlinks smartlink);

        public Task UpdateAsync(Smartlinks smartlink);

        public Task DeleteAsync(Guid id);
    }
}
