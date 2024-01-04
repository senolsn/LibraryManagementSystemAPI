using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Request.DepositBook
{
    public class CreateDepositBookRequest
    {
        public Guid StudentId { get; set; }
        public Guid BookId { get; set; }
        public DateTime DepositDate { get; set; }
        public bool Status { get; set; }
        public DateTime EscrowDate { get; set; }
        public DateTime DateShouldBeEscrow { get; set; }
    }
}
