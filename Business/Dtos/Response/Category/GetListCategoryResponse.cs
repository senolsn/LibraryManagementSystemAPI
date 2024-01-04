using System;

namespace Business.Dtos.Response.Category
{
    public class GetListCategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
