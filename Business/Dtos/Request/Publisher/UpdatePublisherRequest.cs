﻿using System;

namespace Business.Dtos.Request.Publisher
{
    public class UpdatePublisherRequest:IPublisherRequest
    {
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
