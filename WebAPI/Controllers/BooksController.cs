﻿using Business.Abstracts;
using Business.Dtos.Request.BookRequests;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
          
                var result = await _bookService.Add(request);

                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }

                return Ok(result);
          
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

        [HttpGet("GetPaginatedListAsync")]
        public async Task<IActionResult> GetPagedListAsync([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _bookService.GetPaginatedListAsync(pageRequest);

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
                var result = await _bookService.GetListAsync();

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

        [HttpGet("GetPaginatedListAsyncByCategory")]
        public async Task<IActionResult> GetPagedListAsyncByCategory([FromQuery] PageRequest pageRequest, [FromQuery]List<Guid> categoryIds)
        {
            try
            {
                var result = await _bookService.GetPaginatedListAsyncByCategory(pageRequest, categoryIds);

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

        [HttpGet("GetListAsyncByCategory")]
        public async Task<IActionResult> GetListAsyncByCategory([FromQuery] List<Guid> categoryIds)
        {
            try
            {
                var result = await _bookService.GetListAsyncByCategory(categoryIds);

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

        [HttpGet("GetPaginatedListAsyncByLanguage")]
        public async Task<IActionResult> GetPagedListAsyncByLanguage([FromQuery] PageRequest pageRequest, [FromQuery] List<Guid> languageIds)
        {
            try
            {
                var result = await _bookService.GetPaginatedListAsyncByLanguage(pageRequest, languageIds);

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

        [HttpGet("GetListAsyncByLanguage")]
        public async Task<IActionResult> GetListAsyncByLanguage([FromQuery] List<Guid> languageIds)
        {
            try
            {
                var result = await _bookService.GetListAsyncByLanguage(languageIds);

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

        [HttpGet("GetPaginatedListAsyncSortedByName")]
        public async Task<IActionResult> GetListAsyncSortedByName([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _bookService.GetPaginatedListAsyncSortedByName(pageRequest);

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
                var result = await _bookService.GetListAsyncSortedByName();

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

        [HttpGet("GetPaginatedListAsyncSortedByCreatedDate")]
        public async Task<IActionResult> GetPaginatedListAsyncSortedByCreatedDate([FromQuery] PageRequest pageRequest)
        {
            try
            {
                var result = await _bookService.GetPaginatedListAsyncSortedByCreatedDate(pageRequest);

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
                var result = await _bookService.GetListAsyncSortedByCreatedDate();

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
