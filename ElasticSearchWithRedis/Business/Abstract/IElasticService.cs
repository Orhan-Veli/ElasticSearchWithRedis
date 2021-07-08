using ElasticSearchWithRedis.Business.Utilities.Abstract;
using ElasticSearchWithRedis.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Business.Abstract
{
    public interface IElasticService
    {
        Task<IResult<bool>> Create(MachineConnectionInformation model);
        Task<IResult<MachineConnectionInformation>> Update(MachineConnectionInformation model);
        Task<IResult<MachineConnectionInformation>> Get(Guid id);
        Task<IResult<List<MachineConnectionInformation>>> GetAll();
        Task<IResult<bool>> Delete(Guid id);
  
    }
}
