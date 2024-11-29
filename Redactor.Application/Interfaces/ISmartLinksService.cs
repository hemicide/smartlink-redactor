using Redactor.Application.DTO;

namespace Redactor.Application.Interfaces
{
    public interface ISmartLinksService
    {
        public Task<IEnumerable<LinkResponse>> GetAllAsync();
        public Task<LinkResponse> GetByIdAsync(Guid id);
        public Task AddAsync(LinkRequest user);
        public Task UpdateAsync(Guid id, LinkRequest user);
        public Task DeleteAsync(Guid id);
    }
}
