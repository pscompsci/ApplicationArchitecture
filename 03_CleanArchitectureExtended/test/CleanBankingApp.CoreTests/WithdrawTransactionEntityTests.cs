using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanBankingApp.CoreTests
{
    public class WithdrawTransactionEntityTests
    {
        private readonly Account account = new Account("Test", 100);
        
        [Fact]
        public void Withdraw_Create_NotNull()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            Assert.NotNull(withdraw);
        }

        [Fact]
        public void Withdraw_SetId_OK()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            withdraw.SetId(1);
            Assert.Equal(1, withdraw.Id);
        }

        [Fact]
        public void Withdraw_Type_IsWithdraw()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            Assert.Equal("Withdraw", withdraw.Type);
        }

        [Fact]
        public void Withdraw_CreateWithNegativeAmount_ThrowsNegativeAmountException()
        {
            Assert.Throws<NegativeAmountException>(() => new WithdrawTransaction(account, -100));
        }

        [Fact]
        public void Withdraw_CreateWithPositiveAmount_CreatesOKWithPendingStatus()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            Assert.Equal("Pending", withdraw.Status);
        }

        [Fact]
        public void Withdraw_Executes_OK()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            _ = withdraw.Execute();
            Assert.True(withdraw.Success);
        }

        [Fact]
        public void Withdraw_Executes_ExecutedIsTrue()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            _ = withdraw.Execute();
            Assert.True(withdraw.Executed);
        }

        [Fact]
        public void Withdraw_Execute_StatusComplete()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            _ = withdraw.Execute();
            Assert.Equal("Complete", withdraw.Status);
        }

        [Fact]
        public void Withdraw_Execute_BalanceUpdatesOK()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            _ = withdraw.Execute();
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void Withdraw_Rollback_OK()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            _ = withdraw.Execute();
            _ = withdraw.Rollback();
            Assert.True(withdraw.Reversed);
        }

        [Fact]
        public void Withdraw_Rollback_BalanceUpdatesOK()
        {
            WithdrawTransaction withdraw = new WithdrawTransaction(account, 100);
            withdraw.Execute();
            withdraw.Rollback();
            Assert.Equal(100, account.Balance);
        }
    }
}
