using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOBank.Models
{
    public interface IModel
    {
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
        DateTime UpdatedOn { get; set; }
    }
}
