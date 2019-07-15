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
    [Route("api/typ")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class TypController : ControllerBase
    {
        private readonly ILog _log;

        private readonly ICommonService<TypDto> _typService;

        public TypController(ILog log, ICommonService<TypDto> typService)
        {
            _log = log;

            _typService = typService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetTyps()
        {
            var typs = await _typService.GetAll();
            return Ok(typs);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddTyp([FromBody]CommonAddDto typ)
        {
            try
            {
                await _typService.Add(typ);
                return Ok();
            }
            catch (AddingException aex)
            {
                _log.Error(aex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Typ with same name exists.");
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateTyp([FromBody]CommonUpdateDto typ)
        {
            try
            {
                await _typService.Update(typ);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This typ doesn't exist.");
            }
            catch (UpdatingException uex)
            {
                _log.Error(uex);
                return StatusCode((int)HttpStatusCode.Forbidden, "Typ with same name exists.");
            }
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> RemoveTyp([FromBody]int id)
        {
            try
            {
                await _typService.Remove(id, Enums.RemovalType.Typ);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This typ doesn't exist.");
            }
            catch (RemovingException rex)
            {
                _log.Error(rex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This typ is used in the description of the other car.");
            }
        }
    }
}