using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Language:IEntity
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
