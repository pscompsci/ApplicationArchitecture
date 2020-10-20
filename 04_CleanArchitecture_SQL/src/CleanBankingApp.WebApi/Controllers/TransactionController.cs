using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.WebApi.DTO;

namespace CleanBankingApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IAccountService _accounts;
        private readonly ITransactionService _transactions;
        private readonly ITransactionsManager _manager;

        public TransactionsController(
            IAccountService accounts, 
            ITransactionService transactions,
            ITransactionsManager manager
        )
        {
            _accounts = accounts;
            _transactions = transactions;
            _manager = manager;
        }

        [HttpPost]
        public ActionResult<Transaction> CreateTransaction(CreateTransactionDto dto)
        {
            try
            {
                Transaction transaction = _manager.CreateFromHttpPost(dto);
                return transaction;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
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
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:int}/rollback")]
        public ActionResult<Transaction> Rollback(int id)
        {
            try
            {
                Transaction transaction = _manager.Rollback(id);
                return transaction;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}