using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class Setting : BaseEntity
    {
        public Guid SettingId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ExpirationMailHeader { get; set; }
        public string ExpirationMailBody { get; set; }
        public string RemainingMailHeader { get; set; }
        public string RemainingMailBody { get; set; }
        public int BookReturnDay { get; set; }
        public int RemainingDayBeforeBookReturn { get; set; }



    }
}
