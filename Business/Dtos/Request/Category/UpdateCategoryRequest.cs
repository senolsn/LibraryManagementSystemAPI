using System;

namespace Business.Dtos.Request.Category
{
    public class UpdateCategoryRequest
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
