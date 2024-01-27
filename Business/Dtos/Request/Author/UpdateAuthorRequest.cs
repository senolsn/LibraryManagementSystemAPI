using System;

namespace Business.Dtos.Request.AuthorRequests
{
    public class UpdateAuthorRequest
    {
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
