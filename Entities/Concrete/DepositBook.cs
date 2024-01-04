﻿using Core.Entities.Concrete;
using Entities.Concrete.enums;
using System;

namespace Entities.Concrete
{
    public class DepositBook:BaseEntity
    {
        public Guid DepositBookId { get; set; }
        public string StudentId { get; set; }
        public string BookId { get; set; }
        public DateTime DepositDate { get; set; }
        public DepositBookStatus Status { get; set; } = DepositBookStatus.NOT_RECEIVED;
        public DateTime EscrowDate { get; set; }
        public DateTime DateShouldBeEscrow { get; set; }
        
    }
}
