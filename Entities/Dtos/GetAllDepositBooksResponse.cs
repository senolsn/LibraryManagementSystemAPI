using Entities.Concrete;
using Entities.Concrete.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class GetAllDepositBooksResponse
    {
        public Guid DepositBookId { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
        public DateTime DepositDate { get; set; }
        public DepositBookStatus Status { get; set; }
        public DateTime EscrowDate { get; set; }
        public DateTime DateShouldBeEscrow { get; set; }
    }
}
