using System;

namespace Business.Dtos.Request.Location
{
    public class CreateLocationRequest
    {
        public string? Shelf { get; set; }
        public Guid CategoryId { get; set; }
    }
}
