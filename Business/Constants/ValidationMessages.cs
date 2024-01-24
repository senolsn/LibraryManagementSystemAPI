using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class ValidationMessages
    {
        public static string CategoryNotEmpty = "Category name cannot be empty.";
        public static string CategoryMinLength = "Category name must be at least 3 characters long.";

        public static string LanguageNotEmpty = "Language name cannot be empty.";
        public static string LanguageMinLength = "Language name must be at least 3 characters long.";

        public static string FacultyNotEmpty = "Faculty name cannot be empty.";
        public static string FacultyMinLength = "Faculty name must be at least 3 characters long.";
    }
}
