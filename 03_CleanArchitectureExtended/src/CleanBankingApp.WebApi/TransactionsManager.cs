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
    public class TransactionsManager : ITransactionsManager
    {
        private readonly IAccountService _accounts;
        private readonly ITransactionService _transactions;

        public TransactionsManager(IAccountService accounts, ITransactionService transactions)
        {
            _accounts = accounts;
            _transactions = transactions;
        }

        public Account GetById(int id)
        {
            Account account = _accounts.GetById(id);
            if (account is null) throw new AccountDoesNotExistException();
            return account;
        }

        private DepositTransaction CreateDeposit(int id, decimal amount)
        {
            Account account = GetById(id);
            return (DepositTransaction)_transactions.NewDeposit(account, amount);
        }

        private WithdrawTransaction CreateWithdraw(int id, decimal amount)
        {
            Account account = GetById(id);
            return (WithdrawTransaction)_transactions.NewWithdraw(account, amount);
        }

        private TransferTransaction CreateTransfer(int fromId, int toId, decimal amount)
        {
            Account from = GetById(fromId);
            Account to = GetById(toId);
            return (TransferTransaction)_transactions.NewTransfer(from, to, amount);
        }

        public Transaction CreateFromHttpPost(CreateTransactionDto dto)
        {
            if (!ValidateDto(dto)) return null;

            Transaction transaction = dto.Type switch
            {
                TransactionType.Deposit => CreateDeposit(dto.ToAccountId, dto.Amount),
                TransactionType.Withdraw => CreateWithdraw(dto.FromAccountId, dto.Amount),
                TransactionType.Transfer => CreateTransfer(dto.FromAccountId, dto.ToAccountId, dto.Amount),
                _ => null
            };
            return transaction;
        }

        public Transaction Rollback(int id)
        {
            Transaction transaction = _transactions.GetById(id);
            return _transactions.Rollback(transaction);
        }

        private bool ValidateDto(CreateTransactionDto dto)
        {
            if (dto.Type is 0) return false;

            switch(dto.Type)
            {
            case TransactionType.Deposit:
                if (dto.ToAccountId is 0) return false;
                break;
            case TransactionType.Withdraw:
                if (dto.FromAccountId is 0) return false;
                break;
            case TransactionType.Transfer:
                if (dto.FromAccountId is 0 ||
                    dto.ToAccountId is 0
                ) return false;
                break;
            default:
                return false;
            }

            if (dto.Amount <= 0) return false;

            return true;
        }

        public TransactionDetailDto AsTransactionDetailDto(Transaction transaction)
        {
            TransactionDetailDto dto = new TransactionDetailDto();
            switch (transaction.Type)
            {
            case "Deposit":
                DepositTransaction deposit = transaction as DepositTransaction;
                dto.ToAccountId = deposit.Account.Id;
                break;
            case "Withdraw":
                WithdrawTransaction withdraw = transaction as WithdrawTransaction;
                dto.FromAccountId = withdraw.Account.Id;
                break;
            case "Transfer":
                TransferTransaction transfer = transaction as TransferTransaction;
                dto.FromAccountId = transfer.From.Id;
                dto.ToAccountId = transfer.To.Id;
                break;
            default:
                break;
            }
            dto.Id = transaction.Id;
            dto.Type = transaction.Type;
            dto.Amount = transaction.Amount;
            dto.DateStamp = transaction.DateStamp;
            dto.Status = transaction.Status;

            return dto;
        }

        public List<TransactionDetailDto> AsTransactionDetailDtoList(IEnumerable<Transaction> transactions)
        {
            List<TransactionDetailDto> dtos = new List<TransactionDetailDto>();
            foreach(var transaction in transactions)
                dtos.Add(AsTransactionDetailDto(transaction));
            return dtos;
        }
    }
}
