using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.Publisher
{
    public class UpdatePublisherRequest
    {
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
