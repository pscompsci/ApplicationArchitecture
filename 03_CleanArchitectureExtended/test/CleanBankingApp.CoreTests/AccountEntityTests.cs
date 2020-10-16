using System;
using Xunit;
using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Exceptions;

namespace CleanBankingApp.CoreTests
{
    public class AccountEntityTests
    {
        [Fact]
        public void Account_Create_NotNull()
        {
            Account account = new Account("Test", 100);
            Assert.NotNull(account);
        }

        [Fact]
        public void Account_CreateWithNegativeBalance_ThrowsNegativeAmountException()
        {
            Assert.Throws<NegativeAmountException>(() => new Account("Test", -100));
        }

        [Fact]
        public void Account_CreateWithPositiveBalance_CreatesOK()
        {
            Account account = new Account("Test", 100);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void Account_DepositNegativeAmount_ThrowsNegativeAmountException()
        {
            Account account = new Account("Test", 100);
            Assert.Throws<NegativeAmountException>(() => account.Deposit(-100));
        }

        [Fact]
        public void Account_DepositPositiveAmount_ReturnsTrue()
        {
            Account account = new Account("Test", 100);
            bool result = account.Deposit(100);
            Assert.True(result);
        }

        [Fact]
        public void Account_DepositPositveAmount_UpdatesBalanceOK()
        {
            Account account = new Account("Test", 100);
            account.Deposit(100);
            Assert.Equal(200, account.Balance);
        }

        [Fact]
        public void Account_WithdrawtNegativeAmount_ThrowsNegativeAmountException()
        {
            Account account = new Account("Test", 100);
            Assert.Throws<NegativeAmountException>(() => account.Withdraw(-100));
        }

        [Fact]
        public void Account_WithdrawPositiveAmountLessThanBalance_ReturnsTrue()
        {
            Account account = new Account("Test", 100);
            bool result = account.Withdraw(50);
            Assert.True(result);
        }

        [Fact]
        public void Account_WithdrawPositiveAmountEqualToBalance_ReturnsTrue()
        {
            Account account = new Account("Test", 100);
            bool result = account.Withdraw(100);
            Assert.True(result);
        }

        [Fact]
        public void Account_WithdrawMoreThanBalance_ThrowsInsufficientFundsException()
        {
            Account account = new Account("Test", 100);
            Assert.Throws<InsufficientFundsException>(() => account.Withdraw(200));
        }

        [Fact]
        public void Account_WithdrawPositiveAmount_UpdatesBalanceOK()
        {
            Account account = new Account("Test", 100);
            account.Withdraw(50);
            Assert.Equal(50, account.Balance);
        }
    }
}
