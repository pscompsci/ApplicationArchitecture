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
        public ActionResult<TransactionDetailDto> CreateTransaction(CreateTransactionDto dto)
        {
            Transaction transaction = _manager.CreateFromHttpPost(dto);
            if (transaction is null) 
                return NotFound("Structure of the request body is not valid.");
            return _manager.AsTransactionDetailDto(transaction);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TransactionDetailDto>> Get()
        {
            return _manager.AsTransactionDetailDtoList(_transactions.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<TransactionDetailDto> GetById(int id)
        {
            Transaction transaction = _transactions.GetById(id);
            if (transaction is null) 
                return NotFound($"Transaction with Id: {id}, does not exist");
            return _manager.AsTransactionDetailDto(transaction);
        }

        [HttpPut("{id:int}/rollback")]
        public ActionResult<TransactionDetailDto> Rollback(int id)
        {
            Transaction transaction = _manager.Rollback(id);
            if (transaction is null) 
                return NotFound($"Transaction with Id: {id}, does not exist, or already reversed.");
            return _manager.AsTransactionDetailDto(transaction);
        }
    }
}