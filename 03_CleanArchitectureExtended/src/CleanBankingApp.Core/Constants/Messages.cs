namespace CleanBankingApp.Core.Constants
{
    public static class Messages
    {
        public static string InvalidAccountName => "Name cannot be null or empty";
        public static string NegativeAmount => "Amount cannot be less than $0.00";
        public static string NegativeBalance => "Balance cannot be less than $0.00";
        public static string NullAccount => "Account cannot be null";

    }
}