using Business.Abstracts;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Business.Dtos.Request.User;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldUsersController : ControllerBase
    {
        protected readonly IUserService _userService;
        public OldUsersController(IUserService userService)
        {
            _userService = userService;
        }

      

   

       
    }
}
