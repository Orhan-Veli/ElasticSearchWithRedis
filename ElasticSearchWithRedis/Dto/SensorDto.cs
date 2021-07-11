using ElasticSearchWithRedis.Dal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Dto
{
    public class SensorDto
    {
        public string Name { get; set; }
        public Guid SensorId { get; set; }

        public Guid AssetId { get; set; }

        public int Duration { get; set; }

        public SensorType SensorType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
