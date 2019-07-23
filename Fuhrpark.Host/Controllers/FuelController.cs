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
    [Route("api/fuel")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    //[Authorize(Policy = "Bearer")]
    public class FuelController : ControllerBase
    {
        private readonly ILog _log;

        private readonly ICommonService<FuelDto> _fuelService;

        public FuelController(ILog log, ICommonService<FuelDto> fuelService)
        {
            _log = log;

            _fuelService = fuelService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetFuels()
        {
            var fuels = await _fuelService.GetAll();
            return Ok(fuels);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddFuel([FromBody]CommonAddDto fuel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _fuelService.Add(fuel);
                return Ok();
            }
            catch (AddingException aex)
            {
                _log.Error(aex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Fuel with same name exists.");
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateFuel([FromBody]CommonUpdateDto fuel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _fuelService.Update(fuel);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This fuel doesn't exist.");
            }
            catch (UpdatingException uex)
            {
                _log.Error(uex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Fuel with same name exists.");
            }

        }

        [HttpPost]
        [Route("remove/{id}")]
        public async Task<ActionResult> RemoveFuel(int id)
        {
            try
            {
                await _fuelService.Remove(id, Enums.RemovalType.Fuel);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This fuel doesn't exist.");
            }
            catch (RemovingException rex)
            {
                _log.Error(rex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This fuel is used in the description of the other car.");
            }
        }
    }
}