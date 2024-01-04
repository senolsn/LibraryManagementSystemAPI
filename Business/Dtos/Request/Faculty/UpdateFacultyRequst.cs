using System;

namespace Business.Dtos.Request.Faculty
{
    public class UpdateFacultyRequest
    {
        public Guid FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
}
