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
            Account account= null;
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
            Account account= null;
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

        // [HttpPut("{id:int}/deposit/{amount:int}")]
        // public ActionResult<bool> Deposit(int id, int amount)
        // {
        //     Account account = _accounts.GetById(id);
        //     if (account is null) return false;
        //     return _accounts.Deposit(account, amount);
        // }

        // [HttpPut("{id:int}/withdraw/{amount:int}")]
        // public ActionResult<bool> Withdraw(int id, int amount)
        // {
        //     Account account = null;
        //     try
        //     {
        //         account = _accounts.GetById(id);
        //         _accounts.Withdraw(account, amount);
        //     }
        //     catch (AccountDoesNotExistException ex)
        //     {
        //         return Conflict(ex.Message);
        //     }
        //     catch (InsufficientFundsException ex)
        //     {
        //         return Conflict(ex.Message + "\n" +
        //             String.Format("{0,-20}{1:C}\n", "Balance:", account.Balance) +
        //             String.Format("{0,-20}{1:C}\n", "Withdraw Requested:", amount));
        //     }
        //     return true;
        // }
    }
}