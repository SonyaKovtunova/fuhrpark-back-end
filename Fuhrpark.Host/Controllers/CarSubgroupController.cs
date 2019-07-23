using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    [Route("api/car-subgroup")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class CarSubgroupController : ControllerBase
    {
        private readonly ILog _log;

        private readonly ICarSubgroupService _carSubgroupService;

        public CarSubgroupController(ILog log, ICarSubgroupService carSubgroupService)
        {
            _log = log;

            _carSubgroupService = carSubgroupService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetCarSubgroups()
        {
            var carSubgroups = await _carSubgroupService.GetAll();
            return Ok(carSubgroups);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddCarSubgroup([FromBody]CarSubgroupAddDto carSubgroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _carSubgroupService.Add(carSubgroup);
                return Ok();
            }
            catch (AddingException aex)
            {
                _log.Error(aex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Car subgroup with same name exists.");
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateCarSubgroup([FromBody]CarSubgroupUpdateDto carSubgroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _carSubgroupService.Update(carSubgroup);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This car subgroup doesn't exist.");
            }
            catch (UpdatingException uex)
            {
                _log.Error(uex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Car subgroup with same name exists.");
            }
        }
    }
}