using CleanBankingApp.Core.Domain.Entities;
using System.Collections.Generic;

namespace CleanBankingApp.Infrastructure
{
    public static class FakeDB
    {
        public static readonly List<Account> Accounts = new List<Account>();
    }
}