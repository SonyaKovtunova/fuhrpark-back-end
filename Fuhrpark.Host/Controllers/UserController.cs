using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Dtos.Common;
using Fuhrpark.Services.Contracts.Exceptions;
using Fuhrpark.Services.Contracts.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unity;

namespace Fuhrpark.Host.Controllers
{
    [Route("api/user")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class UserController : ControllerBase
    {
        private readonly ILog _log;

        private readonly ICommonService<UserDto> _service;

        public UserController(ILog log, ICommonService<UserDto> service)
        {
            _log = log;

            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddUser([FromBody]CommonAddDto user)
        {
            try
            {
                await _service.Add(user);
                return Ok();
            }
            catch (AddingException aex)
            {
                _log.Error(aex);
                return StatusCode((int)HttpStatusCode.Forbidden, "User with same name exists.");
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateUser([FromBody]CommonUpdateDto user)
        {
            try
            {
                await _service.Update(user);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This user doesn't exist.");
            }
            catch (UpdatingException uex)
            {
                _log.Error(uex);
                return StatusCode((int)HttpStatusCode.Forbidden, "User with same name exists.");
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> RemoveUser([FromBody]int id)
        {
            try
            {
                await _service.Remove(id, Enums.RemovalType.User);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This user doesn't exist.");
            }
            catch (RemovingException rex)
            {
                _log.Error(rex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This user is used in the description of the other car.");
            }
        }
    }
}