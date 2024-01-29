using System;

namespace Business.Dtos.Response.Location
{
    public class GetListLocationResponse
    {
        public Guid LocationId { get; set; }
        public string? Shelf { get; set; }
    }
}
