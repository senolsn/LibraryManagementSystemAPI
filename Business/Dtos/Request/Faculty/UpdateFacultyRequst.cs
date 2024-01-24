using System;

namespace Business.Dtos.Request.Faculty
{
    public class UpdateFacultyRequest : IFacultyRequest
    {
        public Guid FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
}
