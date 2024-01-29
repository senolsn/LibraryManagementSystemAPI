using Business.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Business.Dtos.Request.Location;
using Core.DataAccess.Paging;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class LocationsController : Controller
    {
        protected readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateLocationRequest request)
        {
            try
            {
                var result = await _locationService.Add(request);

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
        public async Task<IActionResult> Update(UpdateLocationRequest request)
        {
            try
            {
                var result = await _locationService.Update(request);

                if (!result.IsSuccess)
                {
                    NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error : {ex.Message}");
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(DeleteLocationRequest request)
        {
            try
            {
                var result = await _locationService.Delete(request);

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
        public async Task<IActionResult> GetAsync(Guid authorId)
        {
            try
            {
                var result = await _locationService.GetAsync(authorId);

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

        [HttpGet("GetPaginatedListAsync")]
        public async Task<IActionResult> GetPaginatedListAsync([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _locationService.GetPaginatedListAsync(pageRequest);

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

        [HttpGet("GetListAsync")]
        public async Task<IActionResult> GetListAsync()
        {
            try
            {
                var result = await _locationService.GetListAsync();

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

        [HttpGet("GetListAsyncSortedByName")]
        public async Task<IActionResult> GetListAsyncSortedByName()
        {
            try
            {
                var result = await _locationService.GetListAsyncSortedByName();

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

        [HttpGet("GetListAsyncSortedByCreatedDate")]
        public async Task<IActionResult> GetListAsyncSortedByCreatedDate()
        {
            try
            {
                var result = await _locationService.GetListAsyncSortedByCreatedDate();

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
    }
}
