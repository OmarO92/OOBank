using OOBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOBank.Service
{
    public interface IAccountService
    {
        Account GetBankAccount(Guid ownerId, Guid accountId);
    }
    public class AccountService : IAccountService
    {
        public Account GetBankAccount(Guid ownerId, Guid accountId)
        {
            var result = new Account();


            return result;
        }
    }
}
