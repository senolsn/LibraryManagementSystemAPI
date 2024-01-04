using Core.Entities.Concrete;
using System;

namespace Entities.Concrete
{
    public class DepositBook:BaseEntity
    {
        public Guid DepositBookId { get; set; }
        public string StudentId { get; set; }
        public string BookId { get; set; }
        public DateTime DepositDate { get; set; }
        public bool Status { get; set; }
        public DateTime EscrowDate { get; set; }
        public DateTime DateShouldBeEscrow { get; set; }
        
    }
}
