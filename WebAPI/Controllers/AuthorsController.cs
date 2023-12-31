using Business.Abstracts;
using Business.Dtos.Request.Create;
using Business.Dtos.Request.Delete;
using Business.Dtos.Request.Update;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthorsController : Controller
    {
        protected readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateAuthorRequest request)
        {
            var result = await _authorService.Add(request);

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateAuthorRequest request)
        {
            try
            {
                var updatedAuthorResponse = await _authorService.Update(request);

                if (updatedAuthorResponse is null)
                {
                    NotFound();
                }

                return Ok(updatedAuthorResponse);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(DeleteAuthorRequest request)
        {
            try
            {
                var deletedAuthorResponse = await _authorService.Delete(request);

                if (!deletedAuthorResponse.IsDeleted)
                {
                    NotFound();
                }

                return Ok(deletedAuthorResponse);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetAsync(Guid authorId)
        {
            try
            {
                var result = await _authorService.GetAsync(authorId);

                if (result is null)
                {
                    return BadRequest();
                }

                    return Ok(result);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetPagedListAsync")]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] PageRequest pageRequest)
        {
            var result = await _authorService.GetListAsync(pageRequest);

            return Ok(result);
        }
    }
}
