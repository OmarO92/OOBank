using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOBank.Service.Responses
{
    public class TransactionResponse
    {
        public bool IsSuccessful { get; set; }
        public string ResultMessage { get; set; }
    }
}
