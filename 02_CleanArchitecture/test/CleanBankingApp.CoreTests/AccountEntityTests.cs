using System;
using Xunit;
using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Exceptions;

namespace CleanBankingApp.CoreTests
{
    public class AccountEntityTests
    {
        [Fact]
        public void Account_CreatesOK()
        {
            Account account= new Account("Test", 100);
            Assert.NotNull(account);
        }

        [Fact]
        public void Account_InitialBalanceNotNegative()
        {
            Account account = new Account("Test", -100);
            Assert.Equal(0, account.Balance);
            
        }

        [Fact]
        public void Account_InitialPositiveBalanceSetsOK()
        {
            Account account = new Account("Test", 100);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void Account_DepositNegativeAmountReturnsFalse()
        {
            Account account = new Account("Test", 100);
            bool result = account.Deposit(-100);
            Assert.False(result);
        }

        [Fact]
        public void Account_DepositPositiveAmountReturnsTrue()
        {
            Account account = new Account("Test", 100);
            bool result = account.Deposit(100);
            Assert.True(result);
        }

        [Fact]
        public void Account_DepositUpdatesBalanceOK()
        {
            Account account = new Account("Test", 100);
            account.Deposit(100);
            Assert.Equal(200, account.Balance);
        }

        [Fact]
        public void Account_WithdrawtNegativeAmountReturnsFalse()
        {
            Account account = new Account("Test", 100);
            bool result = account.Withdraw(-100);
            Assert.False(result);
        }

        [Fact]
        public void Account_WithdrawPositiveAmountLessThanBalanceReturnsTrue()
        {
            Account account = new Account("Test", 100);
            bool result = account.Withdraw(50);
            Assert.True(result);
        }

        [Fact]
        public void Account_WithdrawPositiveAmountEqualToBalanceReturnsTrue()
        {
            Account account = new Account("Test", 100);
            bool result = account.Withdraw(100);
            Assert.True(result);
        }

        [Fact]
        public void Account_WithdrawPositiveMoreThanBalanceThrowsInsufficientFundsException()
        {
            Account account = new Account("Test", 100);
            Assert.Throws<InsufficientFundsException>(() => account.Withdraw(200));
        }

        [Fact]
        public void Account_WithdrawUpdatesBalanceOK()
        {
            Account account = new Account("Test", 100);
            account.Withdraw(50);
            Assert.Equal(50, account.Balance);
        }
    }
}
