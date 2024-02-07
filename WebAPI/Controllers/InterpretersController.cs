using Business.Abstracts;
using Business.Dtos.Request.InterpreterRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Core.DataAccess.Paging;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class InterpretersController : Controller
    {
        protected readonly IInterpreterService _interpreterService;

        public InterpretersController(IInterpreterService interpreterService)
        {
            _interpreterService = interpreterService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateInterpreterRequest request)
        {
            try
            {
                var result = await _interpreterService.Add(request);

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
        public async Task<IActionResult> Update(UpdateInterpreterRequest request)
        {
            try
            {
                var result = await _interpreterService.Update(request);

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
        public async Task<IActionResult> Delete(DeleteInterpreterRequest request)
        {
            try
            {
                var result = await _interpreterService.Delete(request);

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
        public async Task<IActionResult> GetAsync(Guid interpreterId)
        {
            try
            {
                var result = await _interpreterService.GetAsync(interpreterId);

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
                var result = await _interpreterService.GetPaginatedListAsync(pageRequest);

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

