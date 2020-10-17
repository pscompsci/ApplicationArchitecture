using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.Core.Domain.Enums;
using CleanBankingApp.Core.Interfaces;
using CleanBankingApp.Infrastructure.Exceptions;
using CleanBankingApp.WebApi.DTO;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace CleanBankingApp.WebApi
{
    public interface ITransactionsManager
    {
        Account GetById(int id);
        Transaction CreateFromHttpPost(CreateTransactionDto dto);
        Transaction Rollback(int id);
        TransactionDetailDto AsTransactionDetailDto(Transaction transaction);
        List<TransactionDetailDto> AsTransactionDetailDtoList(IEnumerable<Transaction> transactions);
    }
}