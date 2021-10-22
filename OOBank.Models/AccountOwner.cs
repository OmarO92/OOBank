using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOBank.Models
{
    public class AccountOwner : IModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
