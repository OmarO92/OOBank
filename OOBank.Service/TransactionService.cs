using OOBank.Service.Requests;
using OOBank.Service.Responses;
using System;

namespace OOBank.Service
{
    public interface ITransactionService
    {
        TransactionResponse Deposit(TransactionRequest request);
        TransactionResponse Withdraw(TransactionRequest request);
        TransactionResponse Transfer(TransferRequest request);
    }
    public class TransactionService : ITransactionService
    {
        IAccountService _accountService;

        public TransactionService(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public TransactionResponse Deposit(TransactionRequest request)
        {
            var response = new TransactionResponse();
            var bankAccount = _accountService.GetBankAccount(request.OwnerId, request.AccountId);

            bankAccount.Balance += request.Amount;
            bankAccount.UpdatedOn = DateTime.Now;
            response.IsSuccessful = true;
            response.ResultMessage = "Deposit Successful";
            return response;
        }

        public TransactionResponse Transfer(TransferRequest request)
        {
            var response = new TransactionResponse();

            var fromBankAccount = _accountService.GetBankAccount(request.OwnerId, request.FromAccountId);
            var toBankAccount = _accountService.GetBankAccount(request.OwnerId, request.ToAccountId);
            if(request.Amount > fromBankAccount.Balance)
            {
                response.IsSuccessful = false;
                response.ResultMessage = "Insufficient Funds";
            }

            fromBankAccount.Balance -= request.Amount;
            toBankAccount.Balance += request.Amount;
            fromBankAccount.UpdatedOn = DateTime.Now;
            toBankAccount.UpdatedOn = DateTime.Now;
            response.IsSuccessful = true;
            response.ResultMessage = "Transfer Successful";

            return response;
        }

        public TransactionResponse Withdraw(TransactionRequest request)
        {
            var response = new TransactionResponse();

            var bankAccount = _accountService.GetBankAccount(request.OwnerId, request.AccountId);
            if (request.Amount > bankAccount.Balance)
            {
                response.ResultMessage = "Insufficient Funds";
                response.IsSuccessful = false;
                return response;
            }
            if (bankAccount.AccountType == Models.AccountType.IndividualInvestment && request.Amount > 500.00)
            {
                response.ResultMessage = $"Individual Investment Accounts cannot exceed withdrawal limit of $500.00. Requested: {request.Amount}";
                response.IsSuccessful = false;
                return response;
            }

            bankAccount.Balance -= request.Amount;
            bankAccount.UpdatedOn = DateTime.Now;
            response.IsSuccessful = true;
            response.ResultMessage = $"{request.Amount} withdrawn. {bankAccount.Balance} remaining";
            return response;

        }
    }
}
