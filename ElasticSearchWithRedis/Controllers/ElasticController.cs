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
        public async Task<IActionResult> Create([FromBody]MachineConnectionInformation machineConnectionInformation, string indexName)
        {
            var result = await _elasticService.Create(indexName,machineConnectionInformation);
            if (!result.Success) return BadRequest();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string indexName, Guid id)
        {
            var result = await _elasticService.Delete(indexName, id);
            if (!result.Success) return BadRequest();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string indexName, Guid id)
        {
            var result = await _elasticService.Get(indexName, id);
            if (!result.Success || result.Data == null) return BadRequest();
            return Ok(result.Data);
        }
    }
}
