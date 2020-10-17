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

        [HttpPost]
        public ActionResult<Transaction> CreateTransaction(CreateTransactionDto dto)
        {
            try
            {
                Transactions t = new Transactions(_accounts, _transactions);
                return t.CreateFromHttpPost(dto);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> Get()
        {
            return _transactions.GetAll();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Transaction> GetById(int id)
        {
            try
            {
               return _transactions.GetById(id);
            }
            catch (TransactionDoesNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:int}/rollback")]
        public ActionResult<Transaction> Rollback(int id)
        {
            try
            {
                Transactions t = new Transactions(_accounts, _transactions);
                return t.Rollback(id);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}