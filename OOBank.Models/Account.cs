using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOBank.Models
{
    public class Account : IModel
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public double Balance { get; set; }

        public AccountType AccountType { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
