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
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public ActionResult<Account> PostAccount(Account account)
        {
            try
            {
                return Ok(_accountService.CreateAccount(account));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return _accountService.GetAll();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Account> GetById(int id)
        {
            try
            {
                return Ok(_accountService.GetById(id));
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
                return Ok(_accountService.GetByName(name));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }       
    }
}