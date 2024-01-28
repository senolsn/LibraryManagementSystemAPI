using Business.Abstracts;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Business.Dtos.Request.Publisher;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        protected readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreatePublisherRequest request)
        {
            try
            {
                var result = await _publisherService.Add(request);

                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error : {ex.Message}");
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdatePublisherRequest request)
        {
            try
            {
                var result = await _publisherService.Update(request);

                if (!result.IsSuccess)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error : {ex.Message}");
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(DeletePublisherRequest request)
        {
            try
            {
                var result = await _publisherService.Delete(request);
                if (!result.IsSuccess)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var result = await _publisherService.GetAsync(id);

                if (!result.IsSuccess)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error : {ex.Message}");
            }
        }

        [HttpGet("GetPagedListAsync")]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _publisherService.GetPaginatedListAsync(pageRequest);

                if (!result.IsSuccess)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error : {ex.Message}");
            }
        }


    }
}
