using System;

namespace Business.Dtos.Request.Author
{
    public class UpdateAuthorRequest
    {
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
