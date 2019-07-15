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
    [Route("api/engine-oil")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class EngineOilController : ControllerBase
    {
        private readonly ILog _log;

        private readonly ICommonService<EngineOilDto> _service;

        public EngineOilController(ILog log, ICommonService<EngineOilDto> service)
        {
            _log = log;

            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetEngineOils()
        {
            var engineOils = await _service.GetAll();
            return Ok(engineOils);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddEngineOil([FromBody]CommonAddDto engineOil)
        {
            try
            {
                await _service.Add(engineOil);
                return Ok();
            }
            catch (AddingException aex)
            {
                _log.Error(aex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Engine oil with same name exists.");
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateEngineOil([FromBody]CommonUpdateDto engineOil)
        {
            try
            {
                await _service.Update(engineOil);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This engine oil doesn't exist.");
            }
            catch (UpdatingException uex)
            {
                _log.Error(uex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Engine oil with same name exists.");
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> RemoveEngineOil([FromBody]int id)
        {
            try
            {
                await _service.Remove(id, Enums.RemovalType.EngineOil);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This engine oil doesn't exist.");
            }
            catch (RemovingException rex)
            {
                _log.Error(rex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This engine oil is used in the description of the other car.");
            }
        }
    }
}