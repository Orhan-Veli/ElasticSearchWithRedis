using ElasticSearchWithRedis.Business.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
using ElasticSearchWithRedis.Dto;
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
    public class RedisController : ControllerBase
    {
        private readonly IRedisService _redisService;
        private readonly IElasticService _elasticService;
        public RedisController(IRedisService redisService, IElasticService elasticService)
        {
            _redisService = redisService;
            _elasticService = elasticService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var redisResult = await _redisService.Get(id);
            if (!redisResult.Success) return BadRequest();
            var elasticResult = await _elasticService.Get(Guid.Parse(id));
            if (!elasticResult.Success) return BadRequest();            
            var dtoResult = new SensorDto
            {
                Name = redisResult.Data.Name,
                AssetId= elasticResult.Data.AssetId,
                Duration= elasticResult.Data.Duration,
                EndDate=elasticResult.Data.EndDate,
                SensorId = elasticResult.Data.SensorId,
                SensorType= elasticResult.Data.SensorType,
                StartDate = elasticResult.Data.StartDate                
            };
            return Ok(dtoResult);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Sensor sensor)
        {
            var result = await _redisService.Create(sensor);
            if (!result.Success) return BadRequest();
            return Ok();
        }
        [HttpPost("bulkcreate")]
        public async Task<IActionResult> BulkCreate([FromBody] List<Sensor> sensor)
        {
            var result = await _redisService.BulkCreate(sensor);
            if (!result.Success) return BadRequest();
            return Ok(result.Data);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Sensor sensor)
        {
            var result = await _redisService.Update(sensor);
            if (!result.Success) return BadRequest();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _redisService.Delete(id);
            if (!result.Success) return BadRequest();
            return NoContent();
        }
    }
}
