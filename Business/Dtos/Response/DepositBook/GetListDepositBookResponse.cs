﻿using Business.Dtos.Response.BookResponses;
using Business.Dtos.Response.UserResponses;
using Entities.Concrete;
using Entities.Concrete.enums;
using System;

namespace Business.Dtos.Response.DepositBook
{
    public class GetListDepositBookResponse
    {
        public Guid DepositBookId { get; set; }
        public GetListUserResponse User { get; set; }
        public GetListBookResponse Book{ get; set; }
        public DateTime DepositDate { get; set; }
        public DepositBookStatus Status { get; set; }
        public DateTime EscrowDate { get; set; }
        public DateTime DateShouldBeEscrow { get; set; }
    }
}
