﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fuhrpark.Enums;
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
    [Route("api/manufacturer")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class ManufacturerController : ControllerBase
    {
        private readonly ILog _log;

        private readonly ICommonService<ManufacturerDto> _manufacturerService;

        public ManufacturerController(ILog log, ICommonService<ManufacturerDto> manufacturerService)
        {
            _log = log;

            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetManufacturers()
        {
            var manufacturers = await _manufacturerService.GetAll();
            return Ok(manufacturers);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddManufacturer([FromBody]CommonAddDto manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _manufacturerService.Add(manufacturer);
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
        public async Task<ActionResult> UpdateManufacturer([FromBody]CommonUpdateDto manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _manufacturerService.Update(manufacturer);
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

        [HttpPost]
        [Route("remove/{id}")]
        public async Task<ActionResult> RemoveManufacturer(int id)
        {
            try
            {
                await _manufacturerService.Remove(id, Enums.RemovalType.Manufacturer);
                return Ok();
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, ErrorMessage.NOTEXIST.ToString());
            }
            catch (RemovingException rex)
            {
                _log.Error(rex);
                return StatusCode((int)HttpStatusCode.Forbidden, ErrorMessage.ISUSED.ToString());
            }
        }
    }
}