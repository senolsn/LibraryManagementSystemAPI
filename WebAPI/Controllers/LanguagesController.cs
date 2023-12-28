using Business.Abstracts;
using Business.Dtos.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        protected readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateLanguageRequest request)
        {
            var result = await _languageService.Add(request);

            return Ok(result);
        }

        [HttpGet("GetPagedListAsync")]
        public async Task<IActionResult> GetPagedListAsync(
            [FromQuery] int index = 0,
            [FromQuery] int size = 10
        )
        {
            var result = await _languageService.GetListAsync(
                index: index,
                size: size
            );

            return Ok(result);
        }



    }
}
