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
    [Route("api/typ")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Authorize(Policy = "Bearer")]
    public class TypController : ControllerBase
    {
        private readonly IUnityContainer _container;
        private readonly ILog _log;

        private readonly ICommonService<TypDto> _typService;

        public TypController(IUnityContainer container, ILog log, ICommonService<TypDto> typService)
        {
            _container = container;
            _log = log;

            _typService = typService;
        }

        [HttpGet]
        [Route("list")]
        public async Task<ActionResult> GetTyps()
        {
            var typDtos = await _typService.GetAll();
            var typModels = _container.Resolve<ICommonModelMapper<TypDto, TypModel>>().MapCollectionToModel(typDtos);

            return Ok(typModels);
        }

        [HttpPost]
        [Route("add")]
        public async Task<ActionResult> AddTyp([FromBody]TypModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var typDto = _container.Resolve<ICommonModelMapper<TypDto, TypModel>>().MapFromModel(model);

            try
            {
                await _typService.Add(typDto);
            }
            catch (AddingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Typ with same name exists.");
            }

            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult> UpdateManufacturer([FromBody]TypModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var typDto = _container.Resolve<ICommonModelMapper<TypDto, TypModel>>().MapFromModel(model);

            try
            {
                await _typService.Update(typDto);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This typ doesn't exist.");
            }
            catch (UpdatingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "Typ with same name exists.");
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
                await _typService.Remove(model.Id, Enums.RemovalType.Typ);
            }
            catch (ObjectNotFoundException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This typ doesn't exist.");
            }
            catch (RemovingException)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, "This typ is used in the description of the other car.");
            }

            return Ok();
        }
    }
}