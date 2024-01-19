using System;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim:BaseEntity
    {
        public Guid Id { get; set; }
        public Guid OperationClaimId { get; set; }
        public Guid UserId { get; set; }
    }
}
