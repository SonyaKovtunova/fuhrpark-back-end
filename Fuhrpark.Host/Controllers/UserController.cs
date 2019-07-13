using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fuhrpark.Host.Mappers;
using Fuhrpark.Host.Models;
using Fuhrpark.Services.Contracts.Dtos;
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
        private readonly IUnityContainer _container;
        private readonly ILog _log;

        private readonly ICommonService<UserDto> _service;

        public UserController(IUnityContainer container, ILog log, ICommonService<UserDto> service)
        {
            _container = container;
            _log = log;

            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetUsers()
        {
            var userDtos = await _service.GetAll();
            var userModels = _container.Resolve<ICommonModelMapper<UserDto, UserModel>>().MapCollectionToModel(userDtos);

            return Ok(userModels);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddUser([FromBody]UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = _container.Resolve<ICommonModelMapper<UserDto, UserModel>>().MapFromModel(model);

            try
            {
                await _service.Add(userDto);
            }
            catch (AddingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "User with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateUser([FromBody]UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = _container.Resolve<ICommonModelMapper<UserDto, UserModel>>().MapFromModel(model);

            try
            {
                await _service.Update(userDto);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This user doesn't exist.");
            }
            catch (UpdatingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "User with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> RemoveUser([FromBody]CommonRemoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _service.Remove(model.Id, Enums.RemovalType.User);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This user doesn't exist.");
            }
            catch (RemovingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This user is used in the description of the other car.");
            }

            return Ok();
        }
    }
}