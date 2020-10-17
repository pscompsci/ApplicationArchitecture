using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Core.Domain.Entities;
using System;

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
            try
            {
                return Ok(_accounts.CreateAccount(account));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return _accounts.GetAll();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Account> GetById(int id)
        {
            try
            {
                return Ok(_accounts.GetById(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{name}")]
        public ActionResult<Account> GetByName(string name)
        {
            try
            {
                return Ok(_accounts.GetByName(name));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }       
    }
}