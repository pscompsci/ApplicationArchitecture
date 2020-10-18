using CleanBankingApp.Core.Domain.Entities;
using CleanBankingApp.WebApi.DTO;
using System.Collections.Generic;

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