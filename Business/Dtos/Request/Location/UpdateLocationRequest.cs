using System;

namespace Business.Dtos.Request.Location
{
    public class UpdateLocationRequest
    {
        public Guid LocationId { get; set; }
        public string? Shelf { get; set; }
    }
}
