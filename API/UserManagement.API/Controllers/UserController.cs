using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UserManagement.API.BusinessProcess;
using UserManagement.API.Dtos;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IUserBP _userBP;


        public UserController(ILogger<UserController> logger, IMapper mapper, IUserBP userBP)
        {
            _logger = logger;
            _mapper = mapper;
            _userBP = userBP;
        }

        [HttpGet]
        public IActionResult GetUserAsync()

        {
            try
            {
                List<UserResultDto> user = _userBP.GetUsers();
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error adding retrieving user.");
            }
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] AddUserDto request)

        {
            try
            {
                if (request == null || request.FirstName == null || request.LastName == null)
                {
                    return BadRequest();
                }

                List<UserResultDto> user = _userBP.AddUser(request.FirstName, request.LastName);

                return Ok(user);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + "There was an error saving adding new user.");

            }
        }
    }
}

