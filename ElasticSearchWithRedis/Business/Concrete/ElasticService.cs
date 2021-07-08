﻿using ElasticSearchWithRedis.Business.Abstract;
using ElasticSearchWithRedis.Business.Constants;
using ElasticSearchWithRedis.Business.Utilities.Abstract;
using ElasticSearchWithRedis.Business.Utilities.Concrete;
using ElasticSearchWithRedis.Dal.Abstract;
using ElasticSearchWithRedis.Dal.Concrete;
using ElasticSearchWithRedis.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Business.Concrete
{
    public class ElasticService : IElasticService
    {
        private readonly IElasticRepository<MachineConnectionInformation> _elasticRepository;

        public ElasticService(IElasticRepository<MachineConnectionInformation> elasticRepository)
        {
            _elasticRepository = elasticRepository;
        }
        public async Task<IResult<bool>> Create(string indexName,MachineConnectionInformation model)
        {
            if (
                model.AssetId == Guid.Empty ||
                model.Duration == 0 ||
                model.EndDate == null || 
                model.StartDate == null || 
                model.SensorId == Guid.Empty                
                )
            {
                return new Result<bool>(Messages.ModelNotValid, false);
            }
            var result = await _elasticRepository.Create(indexName,model);
            if(result) return new Result<bool>(true);
            return new Result<bool>(Messages.CreateResponseFailed, false);

        }

        public async Task<IResult<bool>> Delete(string indexName, Guid id)
        {
            if (string.IsNullOrEmpty(indexName) && id == Guid.Empty) return new Result<bool>(false);
            var result = await _elasticRepository.Delete(indexName, id);
            if (result) return new Result<bool>(true);
            return new Result<bool>(Messages.CreateResponseFailed, false);

        }

        public async Task<IResult<MachineConnectionInformation>> Get(string indexName, Guid id)
        {
            if(string.IsNullOrEmpty(indexName) && id == Guid.Empty) return new Result<MachineConnectionInformation>(false);
            var response = await _elasticRepository.Get(indexName,id);
            if(response == null) return new Result<MachineConnectionInformation>(false);
            return new Result<MachineConnectionInformation>(true,response);
        }

        public async Task<IResult<List<MachineConnectionInformation>>> GetAll(string indexName)
        {
            if(string.IsNullOrEmpty(indexName)) return new Result<List<MachineConnectionInformation>>(false);
            var response = await _elasticRepository.GetAll(indexName);
            if(response.Count == 0) return new Result<List<MachineConnectionInformation>>(false);
            return new Result<List<MachineConnectionInformation>>(true,response);

        }

        public async Task<IResult<MachineConnectionInformation>> Update(string indexName,MachineConnectionInformation model)
        {
            if (
               model.AssetId == Guid.Empty ||
               model.Duration == 0 ||
               model.EndDate == null ||
               model.StartDate == null ||
               model.SensorId == Guid.Empty || 
               string.IsNullOrEmpty(indexName)
               )
            {
                return new Result<MachineConnectionInformation>(false);
            }
            var response = await _elasticRepository.Update(indexName,model);
            if(response == null) return new Result<MachineConnectionInformation>(false);
            return new Result<MachineConnectionInformation>(true, response);
        }
    }
}
