using Core.Entities.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Language : BaseEntity
    {
        public Guid LanguageId { get; set; }
        public string? LanguageName { get; set; }

    }
}
