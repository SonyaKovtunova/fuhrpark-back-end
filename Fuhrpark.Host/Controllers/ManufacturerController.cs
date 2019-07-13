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
    [Route("api/manufacturer")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IUnityContainer _container;
        private readonly ILog _log;

        private readonly ICommonService<ManufacturerDto> _manufacturerService;

        public ManufacturerController(IUnityContainer container, ILog log, ICommonService<ManufacturerDto> manufacturerService)
        {
            _container = container;
            _log = log;

            _manufacturerService = manufacturerService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetManufacturers()
        {
            var manufacturerDtos = await _manufacturerService.GetAll();
            var manufacturerModels = _container.Resolve<ICommonModelMapper<ManufacturerDto, ManufacturerModel>>().MapCollectionToModel(manufacturerDtos);

            return Ok(manufacturerModels);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddManufacturer([FromBody]ManufacturerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var manufacturerDto = _container.Resolve<ICommonModelMapper<ManufacturerDto, ManufacturerModel>>().MapFromModel(model);

            try
            {
                await _manufacturerService.Add(manufacturerDto);
            }
            catch (AddingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Manufacturer with same name exists.");
            }
            
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateManufacturer([FromBody]ManufacturerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var manufacturerDto = _container.Resolve<ICommonModelMapper<ManufacturerDto, ManufacturerModel>>().MapFromModel(model);

            try
            {
                await _manufacturerService.Update(manufacturerDto);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This manufacturer doesn't exist.");
            }
            catch (UpdatingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Manufacturer with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> RemoveManufacturer([FromBody]CommonRemoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _manufacturerService.Remove(model.Id, Enums.RemovalType.Manufacturer);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This manufacturer doesn't exist.");
            }
            catch (RemovingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This manufacturer is used in the description of the other car.");
            }

            return Ok();
        }
    }
}