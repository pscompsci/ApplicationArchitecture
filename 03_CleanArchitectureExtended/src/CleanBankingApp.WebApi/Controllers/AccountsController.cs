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
            Account result = _accounts.CreateAccount(account);
            return result;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return _accounts.GetAll();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Account> GetById(int id)
        {
            Account account;
            try
            {
                account = _accounts.GetById(id);
            }
            catch (AccountDoesNotExistException ex)
            {
                return Conflict(ex.Message);
            }
            return account;
        }

        [HttpGet("{name}")]
        public ActionResult<Account> GetByName(string name)
        {
            Account account;
            try
            {
                account = _accounts.GetByName(name);
            }
            catch (AccountDoesNotExistException ex)
            {
                return Conflict(ex.Message);
            }
            return account;
        }        
    }
}