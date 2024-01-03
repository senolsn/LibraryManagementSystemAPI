using Business.Abstracts;
using Business.Dtos.Request.Author;
using Business.Dtos.Request.Book;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        protected readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateBookRequest request)
        {
            try
            {
                var result = await _bookService.Add(request);

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
        public async Task<IActionResult> Update(UpdateBookRequest request)
        {
            try
            {
                var result = await _bookService.Update(request);

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
        public async Task<IActionResult> Delete(DeleteBookRequest request)
        {
            try
            {
                var result = await _bookService.Delete(request);

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
        public async Task<IActionResult> GetAsync(Guid bookId)
        {
            try
            {
                var result = await _bookService.GetAsync(bookId);

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
                var result = await _bookService.GetListAsync(pageRequest);

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

        [HttpGet("GetPagedListAsyncByCategory")]
        public async Task<IActionResult> GetPagedListAsyncByCategory([FromQuery] PageRequest pageRequest,Guid categoryId)
        {
            try
            {
                var result = await _bookService.GetListAsyncByCategory(pageRequest,categoryId);

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
        public async Task<IActionResult> GetListAsyncSortedByName([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _bookService.GetListAsyncSortedByName(pageRequest);

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
        public async Task<IActionResult> GetListAsyncSortedByCreatedDate([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _bookService.GetListAsyncSortedByCreatedDate(pageRequest);

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
