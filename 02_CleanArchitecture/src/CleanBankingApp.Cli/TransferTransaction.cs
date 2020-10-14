using System;
using CleanBankingApp.Core.Domain.Entities;

namespace CleanBankingApp
{
    class TransferTransaction : Transaction
    {
        private readonly Account _fromAccount;
        private readonly Account _toAccount;
        private readonly DepositTransaction _deposit;
        private readonly WithdrawTransaction _withdraw;

        public new bool Success { get => _deposit.Success && _withdraw.Success; }

        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) 
            : base(amount, "Transfer")
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _withdraw = new WithdrawTransaction(_fromAccount, _amount);
            _deposit = new DepositTransaction(_toAccount, _amount);
        }

        public override void Print()
        {
            Console.WriteLine(new String('-', 85));
            Console.WriteLine("|{0, -20}|{1, -20}|{2, 20}|{3, 20}|",
                "FROM ACCOUNT", "To ACCOUNT", "TRANSFER AMOUNT", "STATUS");
            Console.WriteLine(new String('-', 85));
            Console.Write("|{0, -20}|{1, -20}|{2, 20}|", _fromAccount.Name, _toAccount.Name, _amount.ToString("C"));
            Console.WriteLine("{0, 20}|", Status);
            Console.WriteLine(new String('-', 85));
        }

        public override void Execute()
        {
            base.Execute();

            try
            {
                _withdraw.Execute();
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine("Transfer failed with reason: " + exception.Message);
                _withdraw.Print();
            }

            if (_withdraw.Success)
            {
                try
                {
                    _deposit.Execute();
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("Transfer failed with reason: " + exception.Message);
                    _deposit.Print();
                    try
                    {
                        _withdraw.Rollback();
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("Withdraw could not be reversed with reason: " + e.Message);
                        _withdraw.Print();
                        return;
                    }
                }
            }
            _success = true;
        }

        public override void Rollback()
        {
            base.Rollback();

            if (Success)
            {
                try
                {
                    _deposit.Rollback();
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("Failed to rollback deposit: "
                        + exception.Message);
                    return;
                }

                try
                {
                    _withdraw.Rollback();
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine("Failed to rollback withdraw: "
                        + exception.Message);
                    return;
                }
            }
        }

        public override string GetAccountName() => _fromAccount.Name + " -> " + _toAccount.Name;
    }
}