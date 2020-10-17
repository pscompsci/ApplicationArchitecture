using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanBankingApp.Core.Domain.Enums;

namespace CleanBankingApp.WebApi.DTO
{
    public class CreateTransactionDto
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
