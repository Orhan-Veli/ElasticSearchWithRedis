using ElasticSearchWithRedis.Business.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticController : ControllerBase
    {
        private readonly IElasticService _elasticService;
        public ElasticController(IElasticService elasticService)
        {
            _elasticService = elasticService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]MachineConnectionInformation machineConnectionInformation)
        {
            var result = await _elasticService.Create(machineConnectionInformation);
            if (!result.Success) return BadRequest();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _elasticService.Delete(id);
            if (!result.Success) return BadRequest();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _elasticService.Get(id);
            if (!result.Success || result.Data == null) return BadRequest();
            return Ok(result.Data);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _elasticService.GetAll();
            if(!response.Success && response.Data == null) return BadRequest();
            return Ok(response.Data);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]MachineConnectionInformation machineConnectionInformation)
        {
            var response = await _elasticService.Update(machineConnectionInformation);
            if (!response.Success && response.Data == null) return BadRequest();
            return Ok(response.Data);
        }
    }
}
