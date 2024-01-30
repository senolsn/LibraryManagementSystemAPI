using Business.Dtos.Request.Author;
using System;

namespace Business.Dtos.Request.AuthorRequests
{
    public class UpdateAuthorRequest:IAuthorRequest
    {
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
