using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanBankingApp.CoreTests
{
    public class TransferTransactionEntityTests
    {
        readonly Account from = new Account("From", 100);
        readonly Account to = new Account("To", 100);

        [Fact]
        public void Transfer_CreateNotNull()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            Assert.NotNull(transfer);
        }

        [Fact]
        public void Transfer_SetId_OK()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            transfer.SetId(1);
            Assert.Equal(1, transfer.Id);
        }

        [Fact]
        public void Transfer_Type_IsTransfer()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            Assert.Equal("Transfer", transfer.Type);
        }

        [Fact]
        public void Transfer_Create_StatusPending()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            Assert.Equal("Pending", transfer.Status);
        }

        [Fact]
        public void Transfer_CreateWithNegativeAmount_ThrowsNegativeAmountException()
        {
            Assert.Throws<NegativeAmountException>(() => new TransferTransaction(from, to, -50));
        }

        [Fact]
        public void Transfer_Executes_OK()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            _ = transfer.Execute();
            Assert.True(transfer.Success);
        }

        [Fact]
        public void Transfer_Executes_ExecutedIsTrue()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            _ = transfer.Execute();
            Assert.True(transfer.Executed);
        }

        [Fact]
        public void Transfer_Executes_StatusComplete()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            _ = transfer.Execute();
            Assert.Equal("Complete", transfer.Status);
        }

        [Fact]
        public void Transfer_Execute_DepositBalanceUpdatesOK()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            _ = transfer.Execute();
            Assert.Equal(150, to.Balance);
        }

        [Fact]
        public void Transfer_Execute_WithdrawBalanceUpdatesOK()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            _ = transfer.Execute();
            Assert.Equal(50, from.Balance);
        }

        [Fact]
        public void Transfer_Rollback_OK()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            _ = transfer.Execute();
            _ = transfer.Rollback();
            Assert.True(transfer.Reversed);
        }

        [Fact]
        public void Transfer_RollbackWithInsufficientFunds_ThrowsInsufficientFundsException()
        {
            TransferTransaction transfer = new TransferTransaction(from, to, 50);
            _ = transfer.Execute();

            WithdrawTransaction withdraw = new WithdrawTransaction(to, to.Balance);
            withdraw.Execute();

            Assert.Throws<InsufficientFundsException>(() => transfer.Rollback());
        }
    }
}
