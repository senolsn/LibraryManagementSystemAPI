using System;

namespace Business.Dtos.Response.Publisher
{
    public class GetListPublisherResponse
    {
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
