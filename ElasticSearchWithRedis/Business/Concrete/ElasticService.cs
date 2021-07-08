using ElasticSearchWithRedis.Business.Abstract;
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
        public async Task<IResult<bool>> Create(MachineConnectionInformation model)
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
            var result = await _elasticRepository.Create(model);
            if(result) return new Result<bool>(true);
            return new Result<bool>(Messages.CreateResponseFailed, false);

        }

        public async Task<IResult<bool>> Delete(Guid id)
        {
            if (id == Guid.Empty) return new Result<bool>(false);
            var result = await _elasticRepository.Delete(id);
            if (result) return new Result<bool>(true);
            return new Result<bool>(Messages.CreateResponseFailed, false);

        }

        public async Task<IResult<MachineConnectionInformation>> Get(Guid id)
        {
            if(id == Guid.Empty) return new Result<MachineConnectionInformation>(false);
            var response = await _elasticRepository.Get(id);
            if(response == null) return new Result<MachineConnectionInformation>(false);
            return new Result<MachineConnectionInformation>(true,response);
        }

        public async Task<IResult<List<MachineConnectionInformation>>> GetAll()
        {
            var response = await _elasticRepository.GetAll();
            if(response.Count == 0) return new Result<List<MachineConnectionInformation>>(false);
            return new Result<List<MachineConnectionInformation>>(true,response);

        }

        public async Task<IResult<MachineConnectionInformation>> Update(MachineConnectionInformation model)
        {
            if (
               model.AssetId == Guid.Empty ||
               model.Duration == 0 ||
               model.EndDate == null ||
               model.StartDate == null ||
               model.SensorId == Guid.Empty
               )
            {
                return new Result<MachineConnectionInformation>(false);
            }
            var response = await _elasticRepository.Update(model);
            if(response == null) return new Result<MachineConnectionInformation>(false);
            return new Result<MachineConnectionInformation>(true, response);
        }
    }
}
