using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanBankingApp.Core.Domain.Enums;

namespace CleanBankingApp.WebApi.DTO
{
    public class TransactionDetailDto
    {
        public int Id { get; set; }
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateStamp { get; set; }
        public string Status { get; set; }
    }
}