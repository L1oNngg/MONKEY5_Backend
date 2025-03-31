using System;

namespace MONKEY5.BusinessObjects.DTOs
{
    public class ServiceDTO
    {
        public Guid ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? UnitType { get; set; }
    }
}
