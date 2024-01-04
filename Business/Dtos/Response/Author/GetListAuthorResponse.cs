using System;

namespace Business.Dtos.Response.Author
{
    public class GetListAuthorResponse
    {
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
