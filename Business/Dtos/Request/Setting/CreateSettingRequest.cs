using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.SettingRequests
{
    public class CreateSettingRequest
    {
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
