using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.Publisher
{
    public class GetListPublisherResponse
    {
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
