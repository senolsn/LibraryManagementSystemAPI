using System;

namespace Business.Dtos.Request.Location
{
    public class CreateLocationRequest:ILocationRequest
    {
        public string? Shelf { get; set; }
    }
}
