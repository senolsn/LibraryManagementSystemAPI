using Business.Abstracts;
using Business.Dtos.Request.Department;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DepartmentsController : Controller
    {
       protected readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CreateDepartmentRequest request)
        {
            try
            {
                var result = await _departmentService.Add(request);

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
        public async Task<IActionResult> Update(UpdateDepartmentRequest request)
        {
            try
            {
                var result = await _departmentService.Update(request);

                if(!result.IsSuccess)
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
        public async Task<IActionResult> Delete(DeleteDepartmentRequest request)
        {
            try
            {
                var result = await _departmentService.Delete(request);
                if(!result.IsSuccess) 
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
        public async Task<IActionResult> GetAsync(Guid departmentId)
        {
            try
            {
                var result = await _departmentService.GetAsync(departmentId);

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
                var result = await _departmentService.GetListAsync(pageRequest);

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
