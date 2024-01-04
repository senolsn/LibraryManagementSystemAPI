using Core.Entities.Concrete;
using Entities.Concrete.enums;
using System;

namespace Entities.Concrete
{
    public class DepositBook:BaseEntity
    {
        public Guid DepositBookId { get; set; }
        public Guid StudentId { get; set; }
        public Guid BookId { get; set; }
        public DateTime DepositDate { get; set; }
        public DepositBookStatus Status { get; set; } = DepositBookStatus.NOT_RECEIVED;
        public DateTime EscrowDate { get; set; }
        public DateTime DateShouldBeEscrow { get; set; }
        
    }
}
