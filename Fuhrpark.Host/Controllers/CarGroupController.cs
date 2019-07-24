using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fuhrpark.Enums;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Exceptions;
using Fuhrpark.Services.Contracts.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fuhrpark.Host.Controllers
{
    [Route("api/car-group")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class CarGroupController : ControllerBase
    {
        private readonly ILog _log;

        private readonly ICarGroupService _carGroupService;

        public CarGroupController(ILog log, ICarGroupService carGroupService)
        {
            _log = log;

            _carGroupService = carGroupService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetCarSubgroups()
        {
            var carGroups = await _carGroupService.GetAll();
            return Ok(carGroups);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddCarGroup([FromBody]CarGroupAddDto carGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _carGroupService.Add(carGroup);
                return Ok();
            }
            catch (AddingException aex)
            {
                _log.Error(aex);
                return StatusCode((int)HttpStatusCode.Forbidden, ErrorMessage.SAMENAME.ToString());
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateCarGroup([FromBody]CarGroupUpdateDto carGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _carGroupService.Update(carGroup);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, ErrorMessage.NOTEXIST.ToString());
            }
            catch (UpdatingException uex)
            {
                _log.Error(uex);
                return StatusCode((int)HttpStatusCode.Forbidden, ErrorMessage.SAMENAME.ToString());
            }
        }
    }
}