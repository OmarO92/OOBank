using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOBank.Models
{
    public class Transaction : IModel
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public TransactionType TransactionType { get; set; }

        public double TransactionAmount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
