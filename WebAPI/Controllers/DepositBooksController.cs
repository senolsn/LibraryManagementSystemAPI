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

        [HttpGet("GetByBookAndUserId")]
        public async Task<IActionResult> GetAsyncByBookAndUserId(Guid bookId, Guid userId)
        {
            try
            {
                var result = await _depositBookService.GetAsyncByBookAndUserId(bookId, userId);

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
                var result = await _depositBookService.GetPaginatedListAsync(pageRequest);

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

        [HttpGet("GetPagedListAsyncUndeposited")]
        public async Task<IActionResult> GetPagedListAsyncUndeposited([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _depositBookService.GetPaginatedListAsyncUndeposited(pageRequest);

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

        [HttpGet("GetPagedListAsyncDeposited")]
        public async Task<IActionResult> GetPagedListAsyncDeposited([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _depositBookService.GetPaginatedListAsyncDeposited(pageRequest);

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

        [HttpGet("GetPagedListAsyncByUser")]
        public async Task<IActionResult> GetPagedListAsyncByUser([FromQuery] PageRequest pageRequest, Guid userId)
        {
            try
            {
                var result = await _depositBookService.GetPaginatedListAsyncByUserId(pageRequest, userId);

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

        [HttpGet("GetPagedListAsyncByBook")]
        public async Task<IActionResult> GetPagedListAsyncByBook([FromQuery] PageRequest pageRequest, Guid bookId)
        {
            try
            {
                var result = await _depositBookService.GetPaginatedListAsyncByBookId(pageRequest, bookId);

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

        [HttpGet("GetPagedListAsyncUndepositedByUser")]
        public async Task<IActionResult> GetPagedListAsyncUndepositedByUser([FromQuery] PageRequest pageRequest, Guid userId)
        {
            try
            {
                var result = await _depositBookService.GetPaginatedListAsyncUndepositedByUserId(pageRequest, userId);

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
