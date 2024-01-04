using System;

namespace Business.Dtos.Request.Publisher
{
    public class UpdatePublisherRequest
    {
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
