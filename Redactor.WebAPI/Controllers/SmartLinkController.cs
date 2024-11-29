using Microsoft.AspNetCore.Mvc;
using Redactor.Application.DTO;
using Redactor.Application.Interfaces;

namespace Redactor.WebAPI.Controllers
{
    [ApiController]
    [Route("links")]
    public class SmartLinkController : ControllerBase
    {
        private readonly ISmartLinksService _linkService;
        private readonly ILogger<SmartLinkController> _logger;

        public SmartLinkController(ILogger<SmartLinkController> logger, ISmartLinksService service)
        {
            _logger = logger;
            _linkService = service;
        }

        [HttpPost(Name = "CreateSmartLink")]
        public async Task<IActionResult> CreateSmartLink([FromBody] LinkRequest request)
        {
            await _linkService.AddAsync(request);
            return NoContent();
        }

        [HttpGet(Name = "GetSmartLinks")]
        public async Task<ActionResult<IEnumerable<LinkResponse>>> GetSmartLinks()
        {
            var links = await _linkService.GetAllAsync();
            return Ok(links);
        }

        [HttpGet("{id}", Name = "GetSmartLink")]
        public async Task<ActionResult<LinkResponse>> GetSmartLink([FromRoute] Guid id)
        {
            var link = await _linkService.GetByIdAsync(id);
            if (link == null) return NotFound();
            return Ok(link);
        }

        [HttpPut("{id}", Name = "UpdateSmartLink")]
        public async Task<IActionResult> UpdateSmartLink([FromRoute] Guid id, [FromBody] LinkRequest request)
        {
            await _linkService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteSmartLink")]
        public async Task<IActionResult> DeleteSmartLink([FromQuery] Guid id)
        {
            await _linkService.DeleteAsync(id);
            return NoContent();
        }
    }
}
