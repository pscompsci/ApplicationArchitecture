using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Infrastructure.Exceptions;

namespace CleanBankingApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accounts;

        public AccountsController(IAccountService accounts)
        {
            _accounts = accounts;
        }

        [HttpPost]
        public ActionResult<Account> PostAccount(Account account)
        {
            if (string.IsNullOrEmpty(account.Name)) 
                return BadRequest("Name is required.");
            
            if (account.Balance < 0)
                return BadRequest("Initial balance required ($0.00 or more");

            return _accounts.CreateAccount(account);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return _accounts.GetAll();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Account> GetById(int id)
        {
            Account account = _accounts.GetById(id);
            if (account is null) 
                return BadRequest($"Account with Id: {id}, does not exist.");
            return account;
        }

        [HttpGet("{name}")]
        public ActionResult<Account> GetByName(string name)
        {
            Account account = _accounts.GetByName(name);
            if (account is null)
                return BadRequest($"Account with Name: {name}, does not exist.");
            return account;
        }        
    }
}