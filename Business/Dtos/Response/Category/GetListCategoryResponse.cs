using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.Category
{
    public class GetListCategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
