namespace CleanBankingApp.Core.Domain.Enums
{
    public enum TransactionType
    {
        Deposit = 1,  // Allows validation of Type not 0 when parsed from JSON in request body
        Withdraw,
        Transfer
    }
}
