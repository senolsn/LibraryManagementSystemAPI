using Business.Abstracts;
using Business.Dtos.Request.Create;
using Business.Dtos.Request.Delete;
using Business.Dtos.Request.Update;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _languageService.GetAsync(id);

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateLanguageRequest request)
        {
            try
            {
                var updatedLanguageResponse = await _languageService.Update(request);

                if(!updatedLanguageResponse.IsUpdated)
                {
                    return NotFound($"Product with Id {request.LanguageId} not found.");
                }

                return Ok(updatedLanguageResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(500,$"Error : {ex.Message }");
            }
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete(DeleteLanguageRequest request)
        {
            try
            {
                var deletedLanguageResponse = await _languageService.Delete(request);

                if (!deletedLanguageResponse.IsDeleted)
                {
                    return NotFound($"Not found.");
                }

                return Ok(deletedLanguageResponse);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
