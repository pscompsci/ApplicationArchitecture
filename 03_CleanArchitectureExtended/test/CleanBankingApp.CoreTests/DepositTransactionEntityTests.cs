using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanBankingApp.CoreTests
{
    public class DepositTransactionEntityTests
    {
        private readonly Account account = new Account("Test", 100);
        
        [Fact]
        public void Deposit_Create_NotNull()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            Assert.NotNull(deposit);
        }

        [Fact]
        public void Deposit_SetId_OK()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            deposit.SetId(1);
            Assert.Equal(1, deposit.Id);
        }

        [Fact]
        public void Deposit_Type_IsDeposit()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            Assert.Equal("Deposit", deposit.Type);
        }

        [Fact]
        public void Deposit_CreateWithNegativeAmount_ThrowsNegativeAmountException()
        {
            Assert.Throws<NegativeAmountException>(() => new DepositTransaction(account, -100));
        }

        [Fact]
        public void Deposit_CreateWithPositiveAmount_CreatesOKWithPendingStatus()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            Assert.Equal("Pending", deposit.Status);
        }

        [Fact]
        public void Deposit_Executes_OK()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            _ = deposit.Execute();
            Assert.True(deposit.Success);
        }

        [Fact]
        public void Deposit_Executes_ExecutedIsTrue()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            _ = deposit.Execute();
            Assert.True(deposit.Executed);
        }

        [Fact]
        public void Deposit_Execute_StatusComplete()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            _ = deposit.Execute();
            Assert.Equal("Complete", deposit.Status);
        }

        [Fact]
        public void Deposit_Execute_BalanceUpdatesOK()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            _ = deposit.Execute();
            Assert.Equal(200, account.Balance);
        }

        [Fact]
        public void Deposit_Rollback_OK()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            _ = deposit.Execute();
            _ = deposit.Rollback();
            Assert.True(deposit.Reversed);
        }

        [Fact]
        public void Deposit_Rollback_BalanceUpdatesOK()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            _ = deposit.Execute();
            _ = deposit.Rollback();
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void Deposit_Rollback_InsufficientFundsThrowsInsufficientFundsException()
        {
            DepositTransaction deposit = new DepositTransaction(account, 100);
            _ = deposit.Execute();

            WithdrawTransaction withdraw = new WithdrawTransaction(account, 150);
            _ = withdraw.Execute();

            Assert.Throws<InsufficientFundsException>(() => deposit.Rollback());
        }
    }
}
