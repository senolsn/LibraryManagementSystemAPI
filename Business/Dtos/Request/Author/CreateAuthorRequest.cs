using Business.Dtos.Request.Author;

namespace Business.Dtos.Request.AuthorRequests
{
    public class CreateAuthorRequest:IAuthorRequest
    {
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
    }
}
