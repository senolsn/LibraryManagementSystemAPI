using Entities.Concrete.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Response.DepositBook
{
    public class GetListDepositBookResponse
    {
        public Guid DepositBookId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime DepositDate { get; set; }
        public DepositBookStatus Status { get; set; }
        public DateTime EscrowDate { get; set; }
        public DateTime DateShouldBeEscrow { get; set; }
    }
}
