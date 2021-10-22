using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOBank.Service.Requests
{
    public class TransferRequest
    {
        public Guid OwnerId { get; set; }
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public double Amount { get; set; }
    }
}
