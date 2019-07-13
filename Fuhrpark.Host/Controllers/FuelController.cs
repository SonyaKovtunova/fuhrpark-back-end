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
    [Route("api/fuel")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class FuelController : ControllerBase
    {
        private readonly IUnityContainer _container;
        private readonly ILog _log;

        private readonly ICommonService<FuelDto> _fuelService;

        public FuelController(IUnityContainer container, ILog log, ICommonService<FuelDto> fuelService)
        {
            _container = container;
            _log = log;

            _fuelService = fuelService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetFuels()
        {
            var fuelDtos = await _fuelService.GetAll();
            var fuelModels = _container.Resolve<ICommonModelMapper<FuelDto, FuelModel>>().MapCollectionToModel(fuelDtos);

            return Ok(fuelModels);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddFuel([FromBody]FuelModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var fuelDto = _container.Resolve<ICommonModelMapper<FuelDto, FuelModel>>().MapFromModel(model);

            try
            {
                await _fuelService.Add(fuelDto);
            }
            catch (AddingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Fuel with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateFuel([FromBody]FuelModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var fuelDto = _container.Resolve<ICommonModelMapper<FuelDto, FuelModel>>().MapFromModel(model);

            try
            {
                await _fuelService.Update(fuelDto);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This fuel doesn't exist.");
            }
            catch (UpdatingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Fuel with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("remove")]
        public async Task<ActionResult> RemoveFuel([FromBody]CommonRemoveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _fuelService.Remove(model.Id, Enums.RemovalType.Fuel);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This fuel doesn't exist.");
            }
            catch (RemovingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This fuel is used in the description of the other car.");
            }

            return Ok();
        }
    }
}