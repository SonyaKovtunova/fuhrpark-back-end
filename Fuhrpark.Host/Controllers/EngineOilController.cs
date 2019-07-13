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
    [Route("api/engine-oil")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class EngineOilController : ControllerBase
    {
        private readonly IUnityContainer _container;
        private readonly ILog _log;

        private readonly ICommonService<EngineOilDto> _service;

        public EngineOilController(IUnityContainer container, ILog log, ICommonService<EngineOilDto> service)
        {
            _container = container;
            _log = log;

            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetEngineOils()
        {
            var engineOilDtos = await _service.GetAll();
            var engineOilModels = _container.Resolve<ICommonModelMapper<EngineOilDto, EngineOilModel>>().MapCollectionToModel(engineOilDtos);

            return Ok(engineOilModels);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddEngineOil([FromBody]EngineOilModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var engineOilDto = _container.Resolve<ICommonModelMapper<EngineOilDto, EngineOilModel>>().MapFromModel(model);

            try
            {
                await _service.Add(engineOilDto);
            }
            catch (AddingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Engine oil with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateEngineOil([FromBody]EngineOilModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var engineOilDto = _container.Resolve<ICommonModelMapper<EngineOilDto, EngineOilModel>>().MapFromModel(model);

            try
            {
                await _service.Update(engineOilDto);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This engine oil doesn't exist.");
            }
            catch (UpdatingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Engine oil with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> RemoveEngineOil([FromBody]CommonRemoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _service.Remove(model.Id, Enums.RemovalType.EngineOil);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This engine oil doesn't exist.");
            }
            catch (RemovingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This engine oil is used in the description of the other car.");
            }

            return Ok();
        }
    }
}