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
    [Route("api/gear-oil")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class GearOilController : ControllerBase
    {
        private readonly ILog _log;

        private readonly ICommonService<GearOilDto> _service;

        public GearOilController(ILog log, ICommonService<GearOilDto> service)
        {
            _log = log;

            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetGearOils()
        {
            var gearOils = await _service.GetAll();
            return Ok(gearOils);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddGearOil([FromBody]CommonAddDto gearOil)
        {
            try
            {
                await _service.Add(gearOil);
                return Ok();
            }
            catch (AddingException aex)
            {
                _log.Error(aex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Gear oil with same name exists.");
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateGearOil([FromBody]CommonUpdateDto gearOil)
        {
            try
            {
                await _service.Update(gearOil);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This gear oil doesn't exist.");
            }
            catch (UpdatingException uex)
            {
                _log.Error(uex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Gear oil with same name exists.");
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> RemoveGearOil([FromBody]int id)
        {
            try
            {
                await _service.Remove(id, Enums.RemovalType.GearOil);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This gear oil doesn't exist.");
            }
            catch (RemovingException rex)
            {
                _log.Error(rex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This gear oil is used in the description of the other car.");
            }
        }
    }
}