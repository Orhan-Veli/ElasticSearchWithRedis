using ElasticSearchWithRedis.Dal.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchWithRedis.Dal.Entity
{
    public class MachineConnectionInformation : IEntity
    {
        public Guid SensorId { get; set; }

        public Guid AssetId { get; set; }

        public int Duration { get; set; }

        public SensorType SensorType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public enum SensorType
    {
        Blue,
        Red,
        Yellow,
        Green,
        Dark,
        Black
    }
}
