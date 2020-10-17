using CleanBankingApp.Core.Domain.Entities;

namespace CleanBankingApp.WebApi.DTO
{
    public class CreateTransactionDto
    {
        public int FromAccountId { get; set; }
        public int ToAccountId {get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}