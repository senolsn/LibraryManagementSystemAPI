using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Publisher:BaseEntity
    {
        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
