using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class Category:BaseEntity
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }
        //public List<BookCategory> BookCategories { get; set; }
    }
}
