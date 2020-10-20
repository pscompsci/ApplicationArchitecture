using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Enums;
using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Infrastructure.Exceptions;
using CleanBankingApp.WebApi.DTO;
using System.Collections.Generic;
using CleanBankingApp.Core;

namespace CleanBankingApp.WebApi
{
    public class TransactionsManager : ITransactionsManager
    {
        private readonly IAccountService _accounts;
        private readonly ITransactionService _transactionService;

        public TransactionsManager(IAccountService accounts, ITransactionService transactions)
        {
            _accounts = accounts;
            _transactionService = transactions;
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
            return (DepositTransaction)_transactionService.NewDeposit(account, amount);
        }

        private WithdrawTransaction CreateWithdraw(int id, decimal amount)
        {
            Account account = GetById(id);
            return (WithdrawTransaction)_transactionService.NewWithdraw(account, amount);
        }

        private TransferTransaction CreateTransfer(int fromId, int toId, decimal amount)
        {
            Account from = GetById(fromId);
            Account to = GetById(toId);
            return (TransferTransaction)_transactionService.NewTransfer(from, to, amount);
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
            Transaction transaction = _transactionService.GetById(id);
            return _transactionService.Rollback(transaction);

        }

        private bool ValidateDto(CreateTransactionDto dto)
        {
            Guard.Against.Null(dto, "Transaction");
            Guard.Against.Negative(dto.Amount, "Amount");
            Guard.Against.Zero((int)dto.Type, "");

            switch (dto.Type)
            {
                case TransactionType.Deposit: 
                    Guard.Against.Zero(dto.ToAccountId, ""); 
                    break;
                case TransactionType.Withdraw:
                    Guard.Against.Zero(dto.FromAccountId, "");
                    break;
                case TransactionType.Transfer:
                    Guard.Against.Zero(dto.ToAccountId, "");
                    Guard.Against.Zero(dto.FromAccountId, "");
                    break;
                default:
                    return false;
            };

            return true;
        }
    }
}
