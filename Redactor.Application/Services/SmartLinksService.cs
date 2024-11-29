using AutoMapper;
using Redactor.Application.DTO;
using Redactor.Application.Interfaces;
using Redactor.Domain.Entities;
using Redactor.Application.Interfaces;
using Redactor.Application.Exceptions;

namespace Redactor.Application.Services
{
    public class SmartLinksService : ISmartLinksService
    {
        private readonly ISmartLinksRepository _linksRepository;
        private readonly IMapper _mapper;

        public SmartLinksService(ISmartLinksRepository linksRepository, IMapper mapper)
        {
            _linksRepository = linksRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LinkResponse>> GetAllAsync()
        {
            var smartlinks = await _linksRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LinkResponse>>(smartlinks);
        }

        public async Task<LinkResponse> GetByIdAsync(Guid id)
        {
            var smartlink = await _linksRepository.GetByIdAsync(id);
            return _mapper.Map<LinkResponse>(smartlink);
        }
        public async Task AddAsync(LinkRequest request)
        {
            var smartlink = await _linksRepository.GetByLinkAsync(request.Link);
            if (smartlink != null)
                throw new BadRequestException(@$"Duplicate link value ""{request.Link}""");

            smartlink = _mapper.Map<Smartlinks>(request);
            await _linksRepository.AddAsync(smartlink);
        }
        public async Task UpdateAsync(Guid id, LinkRequest request)
        {
            var smartlink = _mapper.Map<Smartlinks>(request);
            smartlink.Id = id;
            await _linksRepository.UpdateAsync(smartlink);
        }

        public async Task DeleteAsync(Guid id) => await _linksRepository.DeleteAsync(id);
    }
}
