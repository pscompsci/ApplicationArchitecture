using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Enums;
using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Infrastructure.Exceptions;
using CleanBankingApp.WebApi.DTO;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace CleanBankingApp.WebApi
{
    public class Transactions
    {
        private readonly IAccountService _accounts;
        private readonly ITransactionService _transactions;

        public Transactions(IAccountService accounts, ITransactionService transactions)
        {
            _accounts = accounts;
            _transactions = transactions;
        }

        Account GetById(int id)
        {
            Account account = _accounts.GetById(id);
            if (account is null) throw new AccountDoesNotExistException();
            return account;
        }

        DepositTransaction CreateDeposit(int id, decimal amount)
        {
            Account account = GetById(id);
            return (DepositTransaction)_transactions.NewDeposit(account, amount);
        }

        WithdrawTransaction CreateWithdraw(int id, decimal amount)
        {
            Account account = GetById(id);
            return (WithdrawTransaction)_transactions.NewWithdraw(account, amount);
        }

        TransferTransaction CreateTransfer(int fromId, int toId, decimal amount)
        {
            Account from = GetById(fromId);
            Account to = GetById(toId);
            return (TransferTransaction)_transactions.NewTransfer(from, to, amount);
        }

        public Transaction CreateFromHttpPost(CreateTransactionDto dto)
        {
            Transaction transaction = dto.Type switch
            {
                TransactionType.Deposit => CreateDeposit(dto.ToAccountId, dto.Amount),
                TransactionType.Withdraw => CreateWithdraw(dto.FromAccountId, dto.Amount),
                TransactionType.Transfer => CreateTransfer(dto.FromAccountId, dto.ToAccountId, dto.Amount),
                _ => throw new ArgumentOutOfRangeException()
            };
            return transaction;
        }

        public Transaction Rollback(int id)
        {
            Transaction transaction = _transactions.GetById(id);
            return _transactions.Rollback(transaction);
        }
    }
}
