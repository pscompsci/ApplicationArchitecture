using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Exceptions;
using CleanBankingApp.Infrastructure.Exceptions;
using CleanBankingApp.WebApi.DTO;

namespace CleanBankingApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IAccountService _accounts;
        private readonly ITransactionService _transactions;

        public TransactionsController(IAccountService accounts, ITransactionService transactions)
        {
            _accounts = accounts;
            _transactions = transactions;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> Get()
        {
            return _transactions.GetAll();
        }

        // TODO: This is doing too much. FIX IT!!!
        [HttpGet("{id:int}")]
        public ActionResult<TransactionDetailDto> GetById(int id)
        {
            Transaction transaction = null;
            try
            {
                transaction = _transactions.GetById(id);
            }
            catch (TransactionDoesNotExistException ex)
            {
                return NotFound(ex.Message);
            }
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
                deposit = transaction as DepositTransaction;
                withdraw = transaction as WithdrawTransaction;
                dto.ToAccountId = deposit.Account.Id;
                dto.FromAccountId = withdraw.Account.Id;
                break;
            default:
                break;
            }
            dto.Type = transaction.Type;
            dto.Amount = transaction.Amount;
            dto.Status = transaction.Status;
            dto.DateStamp = transaction.DateStamp;
            return dto;
        }

        // TODO: This is doing too much. FIX IT!!!
        [HttpPost]
        public ActionResult<Transaction> CreateTransaction(CreateTransactionDto dto)
        {
            Account from = null;
            Account to = null;
            switch(dto.Type)
            {
            case "Deposit":
                to = _accounts.GetById(dto.ToAccountId);
                return _transactions.NewDeposit(to, dto.Amount);
            case "Withdraw":
                from = _accounts.GetById(dto.FromAccountId);
                return _transactions.NewWithdraw(from, dto.Amount);
            case "Transfer":
                from = _accounts.GetById(dto.FromAccountId);
                to = _accounts.GetById(dto.ToAccountId);
                return _transactions.NewTransfer(from, to, dto.Amount);
            default:
                break;
            }
            return null;
        }
    }
}