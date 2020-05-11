using System;

namespace CroudSeek.Shared
{
    public class ZoneDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? MaxLatitude { get; set; }
        public double? MinLatitude { get; set; }
        public double? MaxLongitude { get; set; }
        public double? MinLongitude { get; set; }
        public double? MaxAltitude { get; set; }
        public double? MinAltitude { get; set; }
        public double? SpotLatitude { get; set; }
        public double? SpotLongitude { get; set; }
        public double? SpotRadiusMeters { get; set; }
    }
}
