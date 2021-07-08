using ElasticSearchWithRedis.Dal.Entity;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Extensions
{
    public static class Mapping
    {
        public static CreateIndexDescriptor MachineMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<MachineConnectionInformation>
                (M => M.Properties
                (P => P.Keyword(K => K.Name(N => N.SensorId))
                .Text(T => T.Name(N => N.SensorType))
                .Date(T => T.Name(N => N.EndDate))
                .Date(T => T.Name(N => N.StartDate))
                .Number(T => T.Name(N => N.Duration))
                .Text(T => T.Name(N => N.AssetId))
                ));
        }
    }
}
