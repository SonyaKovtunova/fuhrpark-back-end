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
    [Route("api/gear-oil")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class GearOilController : ControllerBase
    {
        private readonly IUnityContainer _container;
        private readonly ILog _log;

        private readonly ICommonService<GearOilDto> _service;

        public GearOilController(IUnityContainer container, ILog log, ICommonService<GearOilDto> service)
        {
            _container = container;
            _log = log;

            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetGearOils()
        {
            var gearOilDtos = await _service.GetAll();
            var gearOilModels = _container.Resolve<ICommonModelMapper<GearOilDto, GearOilModel>>().MapCollectionToModel(gearOilDtos);

            return Ok(gearOilModels);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddGearOil([FromBody]GearOilModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var gearOilDto = _container.Resolve<ICommonModelMapper<GearOilDto, GearOilModel>>().MapFromModel(model);

            try
            {
                await _service.Add(gearOilDto);
            }
            catch (AddingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Gear oil with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateGearOil([FromBody]GearOilModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var gearOilDto = _container.Resolve<ICommonModelMapper<GearOilDto, GearOilModel>>().MapFromModel(model);

            try
            {
                await _service.Update(gearOilDto);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This gear oil doesn't exist.");
            }
            catch (UpdatingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Gear oil with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> RemoveGearOil([FromBody]CommonRemoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _service.Remove(model.Id, Enums.RemovalType.GearOil);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This gear oil doesn't exist.");
            }
            catch (RemovingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This gear oil is used in the description of the other car.");
            }

            return Ok();
        }
    }
}