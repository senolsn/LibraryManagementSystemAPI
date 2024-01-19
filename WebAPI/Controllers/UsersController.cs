﻿using Business.Abstracts;
using Business.Dtos.Request.User;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            try
            {
                var result = await _userService.Add(request);

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
        
        public async Task<IActionResult> Update(UpdateUserRequest request)
        {
            try
            {
                var result = await _userService.Update(request);

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
        [HttpDelete("Delete")]
        
        public async Task<IActionResult> Delete(DeleteUserRequest request)
        {
            try
            {
                var result = await _userService.Delete(request);

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
        
        public async Task<IActionResult> GetAsync(Guid userId)
        {
            try
            {
                var result = await _userService.GetAsync(userId);

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
                var result = await _userService.GetListAsync(pageRequest);

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
        [HttpGet("GetByMail")]
        public async Task<IActionResult> GetByMail(string mail)
        {
            try
            {
                var result = await _userService.GetByMail(mail);

                if (result is null)
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
