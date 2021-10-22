using System;

namespace OOBank.Service.Requests
{
    public class TransactionRequest
    {
        public Guid OwnerId { get; set; }
        public Guid AccountId { get; set; }
        public double Amount { get; set; }
    }
}
