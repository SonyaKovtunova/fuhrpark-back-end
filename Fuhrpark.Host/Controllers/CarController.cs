using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Fuhrpark.Host.Mappers;
using Fuhrpark.Host.Models;
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
    [Route("api/car")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class CarController : ControllerBase
    {
        private readonly IUnityContainer _container;
        private readonly ILog _log;

        private readonly ICarService _carService;

        public CarController(IUnityContainer container, ILog log, ICarService carService)
        {
            _container = container;
            _log = log;

            _carService = carService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<ActionResult> GetCars([FromBody]CarSearchModel model)
        {
            var searchDto = _container.Resolve<ICarSearchModelMapper>().MapFromModel(model);

            var carDtos = await _carService.GetCars(searchDto);
            var carModels = _container.Resolve<ICarModelMapper>().MapCollectionToModel(carDtos);

            return Ok(carModels);
        }

        [HttpGet]
        [Route("detail/{id}")]
        public async Task<ActionResult> GetCarDetails(int id)
        {
            try
            {
                var carDto = await _carService.GetCarById(id);
                var carModel = _container.Resolve<ICarDetailModelMapper>().MapToModel(carDto);

                return Ok(carModel);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This car doesn't exist.");
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddCar([FromBody]CarAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var carDto = _container.Resolve<ICarAddModelMapper>().MapFromModel(model);

            await _carService.AddCar(carDto);
            
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateCar([FromBody]CarUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var carDto = _container.Resolve<ICarUpdateModelMapper>().MapFromModel(model);

            try
            {
                await _carService.UpdateCar(carDto);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This car doesn't exist.");
            }
            
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> RemoveCar([FromBody]RemoveCarRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var carRemoveInfoDto = await _carService.RemoveCar(model.CarId, model.IsCheck, model.ManufacturerIsDelete, model.TypIsDelete, model.FuelIsDelete,
                                model.EngineOilIsDelete, model.GearOilIsDelete, model.UserIsDelete);

                var carRemoveInfoModel = new RemoveCarResponseModel
                {
                    WithSameManufacturer = carRemoveInfoDto.WithSameManufacturer,
                    WithSameTyp = carRemoveInfoDto.WithSameTyp,
                    WithSameFuel = carRemoveInfoDto.WithSameFuel,
                    WithSameEngineOil = carRemoveInfoDto.WithSameEngineOil,
                    WithSameGearOil = carRemoveInfoDto.WithSameGearOil,
                    WithSameUser = carRemoveInfoDto.WithSameUser
                };

                return Ok(carRemoveInfoModel);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This car doesn't exist.");
            }
            
        }
    }
}