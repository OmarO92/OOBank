using Moq;
using NUnit.Framework;
using OOBank.Models;
using OOBank.Service;
using OOBank.Service.Requests;
using OOBank.Service.Responses;
using System;

namespace OOBank.UnitTests
{
    public class TransactionServiceTests
    {
        private readonly Mock<IAccountService> _accountServiceMock = new Mock<IAccountService>();
        private readonly Guid _individualInvestmentAccountId = Guid.NewGuid();
        private readonly Guid _checkingAccountId = Guid.NewGuid();
        private readonly Guid _ownderId = Guid.NewGuid();
        private TransactionService _transactionService;
        private TransactionRequest _transactionRequest;
        private TransactionResponse _transactionResponse;
        private Account _account;
        private Account _checkingAccount;

        [SetUp]
        public void Setup()
        {
            
            _account = new Account
            {
                Id = _individualInvestmentAccountId,
                OwnerId = _ownderId,
                AccountType = AccountType.IndividualInvestment,
                Balance = 500.00,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };
            _checkingAccount = new Account
            {
                AccountType = AccountType.Checking,
                Id = _checkingAccountId,
                OwnerId = _ownderId,
                Balance = 100.00,
                CreatedOn = new DateTime(2020, 01, 01)
            };
            _transactionRequest = new TransactionRequest
            {
                AccountId = _individualInvestmentAccountId,
                Amount = 501.00,
                OwnerId = _ownderId
            };
            _transactionService = new TransactionService(_accountServiceMock.Object);
            _accountServiceMock.Setup(_ => _.GetBankAccount(_ownderId,_individualInvestmentAccountId)).Returns(_account);
            _accountServiceMock.Setup(_ => _.GetBankAccount(_ownderId, _checkingAccountId)).Returns(_checkingAccount);

        }

        [Test]
        public void Test_Withdrawal_IndividualInvestment_LimitReached()
        {
            _transactionResponse = _transactionService.Withdraw(_transactionRequest);
            Assert.That(_transactionResponse, Is.Not.Null);
            Assert.That(!_transactionResponse.IsSuccessful);
        }
        [Test]
        public void Test_Withdrawal_Insufficient_Funds()
        {
            _transactionRequest.Amount = 1000.00;
            _transactionResponse = _transactionService.Withdraw(_transactionRequest);
            Assert.That(_transactionResponse, Is.Not.Null);
            Assert.That(!_transactionResponse.IsSuccessful);
            Assert.That(_transactionResponse.ResultMessage.Equals("Insufficient Funds"));
        }

        [Test]
        public void Test_Withdrawal_Successful()
        {
            var withdrawalAmount = 10.00;
            var expectedBalance = _account.Balance - withdrawalAmount;
            _transactionRequest.Amount = withdrawalAmount;
            _transactionResponse = _transactionService.Withdraw(_transactionRequest);
            Assert.That(_transactionResponse, Is.Not.Null);
            Assert.That(_transactionResponse.IsSuccessful);
            Assert.That(_account.Balance == expectedBalance);
        }
        
        [Test]
        public void Test_Deposit_Successful()
        {
            var depositAmount = 50.00;
            var expectedBalance = _account.Balance + depositAmount;
            _transactionRequest.Amount = depositAmount;
            _transactionResponse = _transactionService.Deposit(_transactionRequest);
            Assert.That(_transactionResponse, Is.Not.Null);
            Assert.That(_transactionResponse.IsSuccessful);
            Assert.That(_account.Balance == expectedBalance);
        }

        [Test]
        public void Test_Transfer_Successful()
        {
            //transfer between 2 accounts to checking 
            var checkingAccountBalance = 100.00;
            var transferAmount = 50.00;
            

            var transferRequest = new TransferRequest
            {
                FromAccountId = _individualInvestmentAccountId,
                ToAccountId = _checkingAccountId,
                Amount = transferAmount,
                OwnerId = _ownderId
            };

            var expectedAccountBalance = _account.Balance - transferAmount;
            var expectedCheckingBalance = checkingAccountBalance + transferAmount;

            _transactionResponse = _transactionService.Transfer(transferRequest);
            Assert.That(_transactionResponse, Is.Not.Null);
            Assert.That(_transactionResponse.IsSuccessful);
            Assert.That(_account.Balance == expectedAccountBalance);
            Assert.That(_checkingAccount.Balance == expectedCheckingBalance);

        }

    }
}