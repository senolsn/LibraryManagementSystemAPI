using Business.Abstracts;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Core.DataAccess.Paging;
using Business.Dtos.Request.StaffRequests;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffsController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateStaffRequest request)
        {
            try
            {
                var result = await _staffService.Add(request);

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

        [HttpGet("GetById")]
        public async Task<IActionResult> GetAsync(Guid staffId)
        {
            try
            {
                var result = await _staffService.GetAsync(staffId);

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

        [HttpGet("GetListAsync")]
        public async Task<IActionResult> GetPagedListAsync()
        {
            try
            {
                var result = await _staffService.GetListAsync();

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
