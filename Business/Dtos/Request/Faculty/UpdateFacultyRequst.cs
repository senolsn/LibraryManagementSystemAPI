using System;

namespace Business.Dtos.Request.FacultyResponses
{
    public class UpdateFacultyRequest : IFacultyRequest
    {
        public Guid FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
}
