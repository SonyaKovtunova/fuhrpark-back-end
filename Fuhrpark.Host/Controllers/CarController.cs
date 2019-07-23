using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    [Route("api/car")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    //[Authorize(Policy = "Bearer")]
    public class CarController : ControllerBase
    {
        private readonly ILog _log;

        private readonly ICarService _carService;

        public CarController(ILog log, ICarService carService)
        {
            _log = log;

            _carService = carService;
        }

        [HttpPost]
        [Route("list")]
        public async Task<ActionResult> GetCars([FromBody]IEnumerable<SearchAttributeDto> searchAttributes)
        {
            var cars = await _carService.GetCars(searchAttributes);
            return Ok(cars);
        }

        [HttpGet]
        [Route("detail/{id}")]
        public async Task<ActionResult> GetCarDetails(int id)
        {
            try
            {
                var car = await _carService.GetCarById(id);
                return Ok(car);
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This car doesn't exist.");
            }
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddCar([FromBody]CarAddDto car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _carService.AddCar(car);
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateCar([FromBody]CarUpdateDto car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _carService.UpdateCar(car);
                return Ok();
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This car doesn't exist.");
            }
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ActionResult> RemoveCar([FromBody]RemoveCarSettingsDto removeSettings)
        {
            try
            {
                var carRemoveInfo = await _carService.RemoveCar(removeSettings);
                return Ok(carRemoveInfo);
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
                return StatusCode((int)HttpStatusCode.Forbidden, "This car doesn't exist.");
            }
        }
    }
}