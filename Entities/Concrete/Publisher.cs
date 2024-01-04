using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class Publisher:BaseEntity
    {
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
