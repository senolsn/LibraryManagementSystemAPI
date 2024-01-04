using Business.Abstracts;
using Business.Dtos.Request.Faculty;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Business.Dtos.Request.DepositBook;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositBooksController : ControllerBase
    {
        protected readonly IDepositBookService _depositBookService;
        public DepositBooksController(IDepositBookService depositBookService)
        {
            _depositBookService = depositBookService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateDepositBookRequest request)
        {
            try
            {
                var result = await _depositBookService.Add(request);

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
        public async Task<IActionResult> Update(UpdateDepositBookRequest request)
        {
            try
            {
                var result = await _depositBookService.Update(request);

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

        [HttpPut("GetBookBack")]
        public async Task<IActionResult> GetBookBack(Guid depositBookId)
        {
            try
            {
                var result = await _depositBookService.GetBookBack(depositBookId);

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
        public async Task<IActionResult> Delete(DeleteDepositBookRequest request)
        {
            try
            {
                var result = await _depositBookService.Delete(request);
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
                var result = await _depositBookService.GetAsync(id);

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
                var result = await _depositBookService.GetListAsync(pageRequest);

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
